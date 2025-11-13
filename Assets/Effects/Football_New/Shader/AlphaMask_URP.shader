
Shader "FxClass/AlphaMask_URP_6000"
{
    Properties
    {
        _AlphaIntensity ("Alpha Intensity", Float) = 1
        [HDR]_MainColor ("Main Color", Color) = (0.6792453,0.6792453,0.6792453,1)

        _MainTex       ("Main Tex", 2D) = "white" {}
        _MainTexUspeed ("Main U Speed", Float) = 0
        _MainTexVspeed ("Main V Speed", Float) = 0

        _SecondTex     ("Second Tex", 2D) = "white" {}
        _SecTexUspeed  ("Second U Speed", Float) = 0
        _SecTexVspeed  ("Second V Speed", Float) = 0

        _MaskTex       ("Mask Tex", 2D) = "white" {}
        _Softedge      ("Soft Edge", Float) = 0.1
    }

    SubShader
    {
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

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM

            #pragma target 3.0
            #pragma vertex   vert
            #pragma fragment frag

            // 建議加上這些 multi_compile，跟 URP 比較合
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

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

                float4 screenPos   : TEXCOORD1;

                float2 uvMain      : TEXCOORD2;
                float2 uvSec       : TEXCOORD3;
                float2 uvMask      : TEXCOORD4;

                UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D(_MainTex);      SAMPLER(sampler_MainTex);
            TEXTURE2D(_SecondTex);    SAMPLER(sampler_SecondTex);
            TEXTURE2D(_MaskTex);      SAMPLER(sampler_MaskTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainColor;

                float4 _MainTex_ST;
                float4 _SecondTex_ST;
                float4 _MaskTex_ST;

                float  _AlphaIntensity;

                float  _MainTexUspeed;
                float  _MainTexVspeed;

                float  _SecTexUspeed;
                float  _SecTexVspeed;

                float  _Softedge;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                float3 posWS = TransformObjectToWorld(IN.positionOS.xyz);
                o.positionHCS = TransformWorldToHClip(posWS);

                o.uv    = IN.uv;
                o.color = IN.color;

                o.screenPos = ComputeScreenPos(o.positionHCS);

                o.uvMain = TRANSFORM_TEX(IN.uv, _MainTex);
                o.uvSec  = TRANSFORM_TEX(IN.uv, _SecondTex);
                o.uvMask = TRANSFORM_TEX(IN.uv, _MaskTex);

                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                // _TimeParameters.x = time in seconds
                float t = _TimeParameters.x;

                float2 uvMain = i.uvMain + float2(_MainTexUspeed, _MainTexVspeed) * t;
                float2 uvSec  = i.uvSec  + float2(_SecTexUspeed,  _SecTexVspeed)  * t;

                half mainR   = SAMPLE_TEXTURE2D(_MainTex,   sampler_MainTex,   uvMain).r;
                half secondR = SAMPLE_TEXTURE2D(_SecondTex, sampler_SecondTex, uvSec ).r;
                half maskR   = SAMPLE_TEXTURE2D(_MaskTex,   sampler_MaskTex,   i.uvMask).r;

                half3 rgb = (i.color.rgb * _MainColor.rgb) * secondR;

                float depthFactor = 1.0;
                #if defined(_CAMERA_DEPTH_TEXTURE)
                    float2 uvScr   = i.screenPos.xy / i.screenPos.w;
                    float  rawZ    = SampleSceneDepth(uvScr);
                    float  scene01 = Linear01Depth(rawZ, _ZBufferParams);
                    float  frag01  = Linear01Depth(i.screenPos.z / i.screenPos.w, _ZBufferParams);

                    float  eps     = max(_Softedge, 1e-5);
                    depthFactor    = saturate( abs(scene01 - frag01) / eps );
                #endif

                half a = saturate(mainR * maskR * secondR * i.color.a * _AlphaIntensity * depthFactor);

                return half4(rgb, a);
            }

            ENDHLSL
        }
    }

    FallBack Off
}









/*
Shader "FxClass/AlphaMask_URP"
{
    Properties
    {
        // 用 ASCII 標籤避免亂碼；保留你原本的屬性名稱
        _AlphaIntensity ("Alpha Intensity", Float) = 1
        [HDR]_MainColor ("Main Color", Color) = (0.6792453,0.6792453,0.6792453,1)

        _MainTex        ("Main Tex",   2D) = "white" {}
        _MainTexUspeed  ("Main U Speed", Float) = 0
        _MianTexVspeed  ("Main V Speed", Float) = 0

        _SecondTex      ("Second Tex", 2D) = "white" {}
        _SecTexUspeed   ("Second U Speed", Float) = 0
        _SecTexVspeed   ("Second V Speed", Float) = 0

        _MaskTex        ("Mask Tex",   2D) = "white" {}
        _Softedge       ("Soft Edge",  Float) = 0.1
    }

    SubShader
    {
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
            // 與原版相同：一般透明混合
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma target 3.0
            #pragma vertex   vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

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
                float4 screenPos   : TEXCOORD1;

                float2 uvMain      : TEXCOORD2;
                float2 uvSec       : TEXCOORD3;
                float2 uvMask      : TEXCOORD4;
            };

            // Textures & samplers
            TEXTURE2D(_MainTex);      SAMPLER(sampler_MainTex);
            TEXTURE2D(_SecondTex);    SAMPLER(sampler_SecondTex);
            TEXTURE2D(_MaskTex);      SAMPLER(sampler_MaskTex);

            // Per-material params (SRP Batcher)
            CBUFFER_START(UnityPerMaterial)
                float4 _MainColor;

                float4 _MainTex_ST;
                float4 _SecondTex_ST;
                float4 _MaskTex_ST;

                float  _AlphaIntensity;

                float  _MainTexUspeed;
                float  _MianTexVspeed;

                float  _SecTexUspeed;
                float  _SecTexVspeed;

                float  _Softedge;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                o.uv          = IN.uv;
                o.color       = IN.color;

                o.screenPos   = ComputeScreenPos(o.positionHCS);

                o.uvMain = TRANSFORM_TEX(IN.uv, _MainTex);
                o.uvSec  = TRANSFORM_TEX(IN.uv, _SecondTex);
                o.uvMask = TRANSFORM_TEX(IN.uv, _MaskTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                // 時間（秒）
                float t = _TimeParameters.x;

                // UV 捲動
                float2 uvMain = i.uvMain + float2(_MainTexUspeed, _MianTexVspeed) * t;
                float2 uvSec  = i.uvSec  + float2(_SecTexUspeed,  _SecTexVspeed)  * t;

                // 取樣（只用 R 通道）
                half mainR   = SAMPLE_TEXTURE2D(_MainTex,   sampler_MainTex,   uvMain).r;
                half secondR = SAMPLE_TEXTURE2D(_SecondTex, sampler_SecondTex, uvSec ).r;
                half maskR   = SAMPLE_TEXTURE2D(_MaskTex,   sampler_MaskTex,   i.uvMask).r;

                // RGB：頂點色 * 主色 * 次貼圖R
                half3 rgb = (i.color.rgb * _MainColor.rgb) * secondR;

                // Soft particles（景深淡出；需 URP Asset 勾 Depth Texture）
                float depthFactor = 1.0;
                #if defined(_CAMERA_DEPTH_TEXTURE)
                    float2 uvScr   = i.screenPos.xy / i.screenPos.w;
                    float  rawZ    = SampleSceneDepth(uvScr);
                    float  scene01 = Linear01Depth(rawZ, _ZBufferParams);
                    float  frag01  = Linear01Depth(i.screenPos.z / i.screenPos.w, _ZBufferParams);

                    float  eps     = max(_Softedge, 1e-5);
                    depthFactor    = saturate( abs(scene01 - frag01) / eps );
                #endif

                // Alpha：主R * 遮罩R * 次R * 頂點Alpha * 係數 * 深度
                half a = saturate(mainR * maskR * secondR * i.color.a * _AlphaIntensity * depthFactor);

                return half4(rgb, a);
            }
            ENDHLSL
        }
    }

    FallBack Off
}

*/