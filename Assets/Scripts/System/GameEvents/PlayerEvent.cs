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
    
    public Action<float> OnPlayerSaveValue;
    public void PlayerSaveValue(float value)
    {
        if (OnPlayerSaveValue != null)
        {
            OnPlayerSaveValue(value);
        }
    }
    
    public Action<Elements> OnPlayerSaveElement;
    public void PlayerSaveElement(Elements element)
    {
        if (OnPlayerSaveElement != null)
        {
            OnPlayerSaveElement(element);
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
    
    public Action<float> OnPlayerHPChange;

    public void PlayerHPChange(float value)
    {
        if (OnPlayerHPChange != null)
        {
            OnPlayerHPChange(value);
        }
    }
    
 
}
