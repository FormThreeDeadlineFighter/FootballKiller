Shader "FxClass/AdditiveMask_URP"
{
    Properties
    {
        [HDR]_MainColor1("主色調", Color) = (1,1,1,1)

        _MainTex1   ("主貼圖",      2D) = "white" {}
        _SecondTex1 ("次要貼圖",    2D) = "white" {}
        _MaskTex1   ("遮罩貼圖",    2D) = "white" {}

        _MainTexUspeed1 ("主貼圖U速度", Float) = 0
        _MainTexVspeed1 ("主貼圖V速度", Float) = 0
        _SecTexUspeed1  ("次貼圖U速度", Float) = 0
        _SecTexVspeed1  ("次貼圖V速度", Float) = 0

        _Softedge1 ("軟邊半徑", Float) = 0.1
    }

    SubShader
    {
        // 使用 URP，透明加亮
        Tags
        {
            "RenderPipeline"="UniversalRenderPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Pass
        {
            Name "UniversalForward"
            Blend One One          // Additive
            ZWrite Off             // 透明不寫深度
            Cull Off               // 雙面

            HLSLPROGRAM

            #pragma target   3.0
            #pragma vertex   vert
            #pragma fragment frag

            // 建議加上這些 multi_compile，跟 URP / Instancing 比較合
            #pragma multi_compile_instancing
            #pragma multi_compile_fog

            // URP 必要的函式庫
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            // 頂點/片元 I/O 結構 --------------------------
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float4 color       : COLOR;
                float4 screenPos   : TEXCOORD1;   // for depth
                float2 uvMain      : TEXCOORD2;
                float2 uvSec       : TEXCOORD3;
                float2 uvMask      : TEXCOORD4;

                UNITY_VERTEX_OUTPUT_STEREO
            };

            // 資源宣告 -----------------------------------
            TEXTURE2D(_MainTex1);    SAMPLER(sampler_MainTex1);
            TEXTURE2D(_SecondTex1);  SAMPLER(sampler_SecondTex1);
            TEXTURE2D(_MaskTex1);    SAMPLER(sampler_MaskTex1);

            // SRP Batcher 友善：材質參數放在 CBUFFER
            CBUFFER_START(UnityPerMaterial)
                float4 _MainColor1;

                float4 _MainTex1_ST;
                float4 _SecondTex1_ST;
                float4 _MaskTex1_ST;

                float  _MainTexUspeed1;
                float  _MainTexVspeed1;
                float  _SecTexUspeed1;
                float  _SecTexVspeed1;

                float  _Softedge1;
            CBUFFER_END

            // 頂點程式 -----------------------------------
            Varyings vert (Attributes IN)
            {
                Varyings o;

                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                float3 posWS = TransformObjectToWorld(IN.positionOS.xyz);
                o.positionHCS = TransformWorldToHClip(posWS);

                o.uv    = IN.uv;
                o.color = IN.color;

                // 供片元計算深度使用
                o.screenPos = ComputeScreenPos(o.positionHCS);

                // 先套用 _ST，再在片元裡做時間偏移
                o.uvMain = TRANSFORM_TEX(IN.uv, _MainTex1);
                o.uvSec  = TRANSFORM_TEX(IN.uv, _SecondTex1);
                o.uvMask = TRANSFORM_TEX(IN.uv, _MaskTex1);

                return o;
            }

            // 片元程式 -----------------------------------
            half4 frag (Varyings i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                // 時間（URP 推薦用 _TimeParameters.x 為秒數）
                float t = _TimeParameters.x;

                // UV 流動（U、V 各自乘時間）
                float2 uvMain = i.uvMain + float2(_MainTexUspeed1, _MainTexVspeed1) * t;
                float2 uvSec  = i.uvSec  + float2(_SecTexUspeed1 , _SecTexVspeed1 ) * t;

                // 取樣三張貼圖（使用 R 通道做灰階遮罩）
                half mainR   = SAMPLE_TEXTURE2D(_MainTex1,   sampler_MainTex1,   uvMain).r;
                half secondR = SAMPLE_TEXTURE2D(_SecondTex1, sampler_SecondTex1, uvSec ).r;
                half maskR   = SAMPLE_TEXTURE2D(_MaskTex1,   sampler_MaskTex1,   i.uvMask).r;

                // 疊乘：Main * Second * Mask
                half maskAll = mainR * secondR * maskR;

                // ---- 深度軟邊（需要在 URP Asset 勾選 Depth Texture）----
                float depthFactor = 1.0;
                #if defined(_CAMERA_DEPTH_TEXTURE)
                    float2 uvScr    = i.screenPos.xy / i.screenPos.w;
                    float  rawScene = SampleSceneDepth(uvScr);
                    float  scene01  = Linear01Depth(rawScene, _ZBufferParams);
                    float  frag01   = Linear01Depth(i.screenPos.z / i.screenPos.w, _ZBufferParams);

                    float  eps      = max(_Softedge1, 1e-5);
                    depthFactor     = saturate( abs(scene01 - frag01) / eps );
                    // 若想靠近物體更亮，可反相：
                    // depthFactor = 1.0 - depthFactor;
                #endif
                // ---------------------------------------------------------

                // 顏色 * 遮罩 * 深度係數  →  加亮輸出
                half3 rgb = (i.color.rgb * _MainColor1.rgb) * maskAll * depthFactor;

                // 在 additive 模式下 alpha 影響較小，但保留頂點 alpha 一致性
                return half4(rgb, i.color.a);
            }

            ENDHLSL
        }
    }

    FallBack Off
}





/*

Shader "FxClass/AdditiveMask_URP"
{
    Properties
    {
        [HDR]_MainColor1("主色調", Color) = (1,1,1,1)

        _MainTex1   ("主貼圖",      2D) = "white" {}
        _SecondTex1 ("次要貼圖",    2D) = "white" {}
        _MaskTex1   ("遮罩貼圖",    2D) = "white" {}

        _MainTexUspeed1 ("主貼圖U速度", Float) = 0
        _MainTexVspeed1 ("主貼圖V速度", Float) = 0
        _SecTexUspeed1  ("次貼圖U速度", Float) = 0
        _SecTexVspeed1  ("次貼圖V速度", Float) = 0

        _Softedge1 ("軟邊半徑", Float) = 0.1
    }

    SubShader
    {
        // 使用 URP，透明加亮
        Tags
        {
            "RenderPipeline"="UniversalRenderPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Pass
        {
            Name "UniversalForward"
            Blend One One          // Additive
            ZWrite Off             // 透明不寫深度
            Cull Off               // 雙面
            HLSLPROGRAM

            #pragma vertex   vert
            #pragma fragment frag
            #pragma target   3.0

            // URP 必要的函式庫
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            // 頂點/片元 I/O 結構 --------------------------
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float4 color       : COLOR;
                float4 screenPos   : TEXCOORD1;   // for depth
                float2 uvMain      : TEXCOORD2;
                float2 uvSec       : TEXCOORD3;
                float2 uvMask      : TEXCOORD4;
            };

            // 資源宣告 -----------------------------------
            TEXTURE2D(_MainTex1);    SAMPLER(sampler_MainTex1);
            TEXTURE2D(_SecondTex1);  SAMPLER(sampler_SecondTex1);
            TEXTURE2D(_MaskTex1);    SAMPLER(sampler_MaskTex1);

            // SRP Batcher 友善：材質參數放在 CBUFFER
            CBUFFER_START(UnityPerMaterial)
                float4 _MainColor1;

                float4 _MainTex1_ST;
                float4 _SecondTex1_ST;
                float4 _MaskTex1_ST;

                float  _MainTexUspeed1;
                float  _MainTexVspeed1;
                float  _SecTexUspeed1;
                float  _SecTexVspeed1;

                float  _Softedge1;
            CBUFFER_END

            // 頂點程式 -----------------------------------
            Varyings vert (Attributes IN)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                o.uv          = IN.uv;
                o.color       = IN.color;

                // 供片元計算深度使用
                o.screenPos   = ComputeScreenPos(o.positionHCS);

                // 先套用 _ST，再在片元裡做時間偏移
                o.uvMain = TRANSFORM_TEX(IN.uv, _MainTex1);
                o.uvSec  = TRANSFORM_TEX(IN.uv, _SecondTex1);
                o.uvMask = TRANSFORM_TEX(IN.uv, _MaskTex1);
                return o;
            }

            // 片元程式 -----------------------------------
            half4 frag (Varyings i) : SV_Target
            {
                // 時間（URP 推薦用 _TimeParameters.x 為秒數）
                float t = _TimeParameters.x;

                // UV 流動（U、V 各自乘時間）
                float2 uvMain = i.uvMain + float2(_MainTexUspeed1, _MainTexVspeed1) * t;
                float2 uvSec  = i.uvSec  + float2(_SecTexUspeed1 , _SecTexVspeed1 ) * t;

                // 取樣三張貼圖（使用 R 通道做灰階遮罩）
                half mainR   = SAMPLE_TEXTURE2D(_MainTex1,   sampler_MainTex1,   uvMain).r;
                half secondR = SAMPLE_TEXTURE2D(_SecondTex1, sampler_SecondTex1, uvSec ).r;
                half maskR   = SAMPLE_TEXTURE2D(_MaskTex1,   sampler_MaskTex1,   i.uvMask).r;

                // 疊乘：Main * Second * Mask
                half maskAll = mainR * secondR * maskR;

                // ---- 深度軟邊（需要在 URP Asset 勾選 Depth Texture）----
                float depthFactor = 1.0;
                #if defined(_CAMERA_DEPTH_TEXTURE)
                    float2 uvScr       = i.screenPos.xy / i.screenPos.w;
                    float  rawScene    = SampleSceneDepth(uvScr);
                    float  scene01     = Linear01Depth(rawScene, _ZBufferParams);
                    float  frag01      = Linear01Depth(i.screenPos.z / i.screenPos.w, _ZBufferParams);

                    float  eps         = max(_Softedge1, 1e-5);
                    depthFactor        = saturate( abs(scene01 - frag01) / eps );
                    // 若想靠近物體更亮，可反相： depthFactor = 1.0 - depthFactor;
                #endif
                // ---------------------------------------------------------

                // 顏色 * 遮罩 * 深度係數  →  加亮輸出
                //half3 rgb = _MainColor1.rgb * maskAll * depthFactor;
                half3 rgb = (i.color.rgb * _MainColor1.rgb) * maskAll * depthFactor;

                //return half4(rgb, 1);
                return half4(rgb, i.color.a);   // 加亮模式下 alpha 影響較小，但可保留一致性
            }

            ENDHLSL
        }
    }

    FallBack Off
}

                */