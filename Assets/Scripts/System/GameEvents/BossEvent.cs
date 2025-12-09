using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannels/BossEvent", fileName = "BossEvent")]
public class BossEvent : ScriptableObject
{
    
    public Action<float> OnBossHPCahange;

    public void BossHPCahange(float value)
    {
        if (OnBossHPCahange != null)
        {
            OnBossHPCahange(value);
        }
    }
}
