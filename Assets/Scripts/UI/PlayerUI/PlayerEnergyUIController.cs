using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyUIController : MonoBehaviour
{
    [SerializeField] Image EnergyUI;
    [SerializeField] PlayerEvent _playerEvents;
    void OnEnable()
    {
        _playerEvents.OnPlayerSaveValue += EnergyUIValueChange;
        _playerEvents.OnPlayerSaveElement += EnergyUIColorChange;
    }
    void OnDisable()
    {
        _playerEvents.OnPlayerSaveValue -= EnergyUIValueChange;
        _playerEvents.OnPlayerSaveElement -= EnergyUIColorChange;
    }
    
    private void EnergyUIValueChange(float value)
    {
        EnergyUI.fillAmount = value;
    }
    
    private void EnergyUIColorChange(Elements Elements)
    {
        Image image = EnergyUI.GetComponent<Image>();
        
        switch(Elements)
        {
            case Elements.white:
                image.color = Color.white;
                break;
            case Elements.black:
                image.color = Color.black;
                break;
            default:
                image.color = Color.white;
                break;
        }
    }
}
