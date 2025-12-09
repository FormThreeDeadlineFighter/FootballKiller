using UnityEngine;
using UnityEngine.UI;

public class BossHPUIController : MonoBehaviour
{
    [SerializeField] Image HPUI;
    [SerializeField] BossEvent _bossEvent;
    
    void OnEnable()
    {
        _bossEvent.OnBossHPCahange += HPUICahnge;
    }
    void OnDisable()
    {
        _bossEvent.OnBossHPCahange -= HPUICahnge;
    }
    
    private void HPUICahnge(float value)
    {
        HPUI.fillAmount = value;
    }
}
