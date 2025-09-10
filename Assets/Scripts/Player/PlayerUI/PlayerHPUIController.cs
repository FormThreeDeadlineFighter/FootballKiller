using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerHPUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] Slider HPUI;
    [SerializeField] PlayerEvents _playerEvents;
    void OnEnable()
    {
        _playerEvents.OnPlayerHurt += HPUICahnge;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerHurt -= HPUICahnge;
    }
    
    private void HPUICahnge(float damage)
    {
        if(HPUI.value >= 0)
        {
            HPUI.value -= damage;
        }
        HPText.text = HPUI.value.ToString();  
    }
}
