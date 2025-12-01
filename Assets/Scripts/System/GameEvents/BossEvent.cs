using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannels/BossEvent", fileName = "BossEvent")]
public class BossEvent : ScriptableObject
{
    public Action<float> OnBossHurt;

    public void BossHurt(float damage)
    {
        if (OnBossHurt != null)
        {
            OnBossHurt(damage);
        }
    }
}
