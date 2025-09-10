using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannels/PlayerEvents", fileName = "PlayerEvents")]
public class PlayerEvents : ScriptableObject
{
    public Action<float> OnPlayerBlock;
    public void PlayerBlock(float damage)
    {
        if (OnPlayerBlock != null)
        {
            OnPlayerBlock(damage);
        }
    }
    
    public Action<float> OnPlayerHurt;

    public void PlayerHurt(float damage)
    {
        if (OnPlayerHurt != null)
        {
            OnPlayerHurt(damage);
        }
    }
    
 
}
