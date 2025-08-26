using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents current;

    void Awake()
    {
        if(current != null)
        {
            DestroyImmediate(this.gameObject);
        }
        current = this;
    }

    public Action<Elements> OnPlayerBlock;
    public void PlayerBlock(Elements elements)
    {
        if (OnPlayerBlock != null)
        {
            OnPlayerBlock.Invoke(elements);
        }
    }
    public Action<float> OnPLayerHurt;
    
    public void PlayerHurt(float damage)
    {
        if (OnPLayerHurt != null)
        {
            OnPLayerHurt(damage);
        }
    }
    
    
}
