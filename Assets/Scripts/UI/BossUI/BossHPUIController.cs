using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossHPUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] Slider HPUI;
    void OnEnable()
    {
        //_playerEvents.OnPlayerHurt += HPUICahnge;
    }
    void OnDisable()
    {
        //_playerEvents.OnPlayerHurt -= HPUICahnge;
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
