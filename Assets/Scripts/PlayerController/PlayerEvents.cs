using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannels/PlayerEvents", fileName = "PlayerEvents")]
public class PlayerEvents : ScriptableObject
{
    public Action<Elements> OnPlayerBlock;
    public void PlayerBlock(Elements elements)
    {
        if (OnPlayerBlock != null)
        {
            OnPlayerBlock(elements);
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
