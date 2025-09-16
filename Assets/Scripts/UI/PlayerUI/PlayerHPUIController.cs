using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHPUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] Slider HPUI;
    [SerializeField] PlayerEvent _playerEvents;
    void OnEnable()
    {
        HPUI.value = HPUI.maxValue;
        HPText.text = HPUI.value.ToString();
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
