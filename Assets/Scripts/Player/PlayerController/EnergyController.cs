using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnergyController : MonoBehaviour
{
    private float _energySaveValue;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _blockEnergy = 10f;
    [SerializeField] private float _minBlockRequired = 5f;
    [SerializeField] private float _shootEnergy = 10f;
    [SerializeField] private float _minShootRequired = 0f;

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
        // reload energy ui
        _playerEvents.PlayerSaveValue(0);
        ElementReset();

    }
    
    void OnDisable()
    {
        _playerEvents.OnPlayerBlock -= OnBlockDetectElement;

        _savedElement = Elements.none;
        _blockEnergy = 0;
    }

    void OnBlockDetectElement(Elements element)
    {
        if (_savedElement == Elements.none)
        {
            Debug.Log($"detect {element}");
            _savedElement = element; 
        }
        // if player have color energy
        // then new energy is no old energy
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
        _playerEvents.PlayerSaveValue(_energySaveValue);
    }
    // player press shoot
    public void OnShoot()
    {
        IAttack playerAttack = _energyBullet.GetComponent<IAttack>();
        playerAttack.Elements = _savedElement;
        
        // if player no elements color energy 
        // shoot to gain energy
        if(_savedElement == Elements.none)
        {
            EnergyGain(5);
            playerAttack.Damage = 1f;
        }
        // if player have elements color energy 
        // shoot to loss energy
        else
        {
            EnergyUse(_shootEnergy);
            playerAttack.Damage = 20f;
        }

        // player bullet material change;
        Instantiate(_energyBullet, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
        
        // Reload element ui
        ElementReset();
        _playerEvents.PlayerSaveValue(_energySaveValue);
    }

    public void EnergyGain(float value)
    {
        ElementReset();
        _energySaveValue += value;
      
        if(_energySaveValue > _maxEnergy)
        {
            _energySaveValue = _maxEnergy;
        }
        
        _playerEvents.PlayerSaveValue(_energySaveValue);
        _playerEvents.PlayerSaveElement(_savedElement);
    }

    public void EnergyUse(float value)
    {
        _energySaveValue -= value;
        
        if(_energySaveValue <= 0)
        {
            _energySaveValue = 0;
        }
        
        _playerEvents.PlayerSaveValue(_energySaveValue);
        _playerEvents.PlayerSaveElement(_savedElement);
    }

    private void ElementReset()
    {
        if (_energySaveValue <= 0)
        {
            _savedElement = Elements.none;
            _playerEvents.PlayerSaveElement(_savedElement);
        }
        
    }
}
