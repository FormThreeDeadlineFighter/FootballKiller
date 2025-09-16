using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossHPUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] Slider HPUI;
    [SerializeField] BossEvent _bossEvent;
    void OnEnable()
    {
        HPUI.value = HPUI.maxValue;
        HPText.text = HPUI.value.ToString();
        _bossEvent.OnBossHurt += HPUICahnge;
    }
    void OnDisable()
    {
        _bossEvent.OnBossHurt -= HPUICahnge;
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
