using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannels/PlayerEvents", fileName = "PlayerEvents")]
public class PlayerEvent : ScriptableObject
{
    public Action<Elements> OnPlayerBlock;
    public void PlayerBlock(Elements element)
    {
        if (OnPlayerBlock != null)
        {
            OnPlayerBlock(element);
        }
    }
    public Action<float> OnPlayerSave;
    public void PlayerSave(float damage)
    {
        if (OnPlayerSave != null)
        {
            OnPlayerSave(damage);
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
