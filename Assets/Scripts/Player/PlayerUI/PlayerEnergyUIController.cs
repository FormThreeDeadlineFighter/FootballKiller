using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerEnergyUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnergyText;
    [SerializeField] Slider EnergyUI;
    [SerializeField] PlayerEvents _playerEvents;
    void OnEnable()
    {
        _playerEvents.OnPlayerBlock += EnergyUICahnge;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerBlock -= EnergyUICahnge;
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
