using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerEnergyUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EnergyText;
    [SerializeField] GameObject EnergyUI;
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
        EnergyUI.transform.localScale = new Vector3(value / 100, 1, 1);
        EnergyText.text = value.ToString("00");  
    }
    
    private void EnergyUIColorChange(Elements Elements)
    {
        Image image = EnergyUI.GetComponent<Image>();
        
        switch(Elements)
        {
            case Elements.red:
                image.color = Color.red;
                break;
            case Elements.green:
                image.color = Color.green;
                break;
            case Elements.blue:
                image.color = Color.blue;
                break;
            default:
                image.color = Color.white;
                break;
        }
    }
}
