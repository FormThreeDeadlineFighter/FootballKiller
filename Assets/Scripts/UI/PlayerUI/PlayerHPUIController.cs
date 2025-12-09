using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUIController : MonoBehaviour
{
    [SerializeField] Image HPUI;
    [SerializeField] PlayerEvent _playerEvents;
    void OnEnable()
    {
        _playerEvents.OnPlayerHPChange += HPUICahnge;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerHPChange -= HPUICahnge;
    }
    
    private void HPUICahnge(float value)
    {
        value = value * 0.9f + 0.1f;
        HPUI.fillAmount = value;
    }
}
