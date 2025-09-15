using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerEnergyUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnergyText;
    [SerializeField] Slider EnergyUI;
    [SerializeField] PlayerEvent _playerEvents;
    void OnEnable()
    {
        _playerEvents.OnPlayerSave += EnergyUICahnge;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerSave -= EnergyUICahnge;
    }
    
    private void EnergyUICahnge(float value)
    {
        if(EnergyUI.value >= 0)
        {
            EnergyUI.value = value;
        }
        EnergyText.text = EnergyUI.value.ToString();  
    }
}
