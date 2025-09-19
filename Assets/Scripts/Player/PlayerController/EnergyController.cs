using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnergyController : MonoBehaviour
{
    private float _energySaveValue;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _blockEnergy = 10f;
    [SerializeField] private float _minBlockRequired = 1f;
    [SerializeField] private float _shootEnergy = 40f;
    [SerializeField] private float _minShootRequired = 30f;

    [SerializeField] private Elements _savedElement;
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _shootingPoint;
    [SerializeField] GameObject _energyBullet;
    [SerializeField] PlayerEvent _playerEvents;

    private PlayerBlockDetector _playerBlockDetector;

    public bool CanBlock => _energySaveValue >= _minBlockRequired;
    public bool CanShoot => _energySaveValue >= _minShootRequired;

    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
    }
    void OnEnable()
    {
        _playerEvents.OnPlayerBlock += OnBlockDetectElement;
        _playerEvents.PlayerSave(_energySaveValue);

    }
    
    void OnDisable()
    {
        _playerEvents.OnPlayerBlock -= OnBlockDetectElement;
    }

    void OnBlockDetectElement(Elements element)
    {
        if (_savedElement == Elements.none)
        {
            Debug.Log($"detect {element}");
            _savedElement = element; 
        }
        
        if(element != _savedElement)
        {      
            Debug.Log("save fail");
            _playerEvents.PlayerHurt(_playerBlockDetector.AttackDamage); 
        }
        else
        {
            if (_energySaveValue < _maxEnergy)
            {
                EnergyGain(_playerBlockDetector.AttackDamage);
                Debug.Log($"save {element} energy");
            }
            else
            {
                Debug.Log("energy full");
            }             
        }     

        ElementReset();
        _playerEvents.PlayerSave(_energySaveValue);
    }
    // player press shoot
    public void OnShoot()
    {
        EnergyUse(_shootEnergy);

        // player bullet material change;
        _energyBullet.GetComponent<IAttack>().Elements = _savedElement;
        Instantiate(_energyBullet, _shootingPoint.transform.position, _shootingPoint.transform.rotation);

        ElementReset();
        _playerEvents.PlayerSave(_energySaveValue);
    }

    public void EnergyGain(float value)
    {
        ElementReset();

        if (_energySaveValue < _maxEnergy)
        {
            _energySaveValue += value;
        }

        _playerEvents.PlayerSave(_energySaveValue);
    }

    public void EnergyUse(float value)
    {
        if(_energySaveValue >= 0)
        {
           _energySaveValue -= value;
        }
        else
        {
            _energySaveValue = 0;
            ElementReset();
        }
   
        _playerEvents.PlayerSave(_energySaveValue);
    }

    private void ElementReset()
    {
        if (_energySaveValue <= 0)
        {
            _savedElement = Elements.none;
        }
    }
}
