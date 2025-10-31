using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnergyController : MonoBehaviour
{
    private float _energySaveValue;
    [Header("Setting")]
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _blockEnergy = 10f;
    [SerializeField] private float _minBlockRequired = 5f;
    [SerializeField] private Elements _savedElement;
    
    [Header("Objects")]
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _playerShootingPoint;
    [SerializeField] GameObject _robotShootingPoint;
    [SerializeField] GameObject _energyBullet;
    [SerializeField] GameObject _robotBullet;
    [SerializeField] PlayerEvent _playerEvents;

    private PlayerBlockDetector _playerBlockDetector;

    public bool CanBlock => _energySaveValue >= _minBlockRequired;
    public bool CanShoot => _energySaveValue >= 0;

    private bool _robotShootCoolDown = true;

    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
    }
    void OnEnable()
    {
        _playerEvents.OnPlayerBlock += OnBlock;
        // reload energy ui
        _playerEvents.PlayerSaveValue(0);
        ElementReset();

    }
    
    void OnDisable()
    {
        _playerEvents.OnPlayerBlock -= OnBlock;

        _savedElement = Elements.none;
        _blockEnergy = 0;
    }

    void OnBlock(Elements element)
    {
        if (_energySaveValue < _maxEnergy)
        {
            _savedElement = element;
            EnergyGain(_playerBlockDetector.AttackDamage * 0.5f);
            Debug.Log($"save {element} energy");
        }
        else
        {
            Debug.Log("energy full");
        }             
        
        _playerEvents.PlayerSaveValue(_energySaveValue);
    }
    
    // player press shoot
    public void OnPlayerShoot()
    {
        //shoot damage anad energy change
        IAttack playerAttack = _energyBullet.GetComponent<IAttack>();
        playerAttack.Elements = _savedElement;
        playerAttack.Damage = _energySaveValue;
      
        // shoot to loss energy 
        EnergyUse(_energySaveValue);
       
        // player bullet material change;
        Instantiate(_energyBullet, _playerShootingPoint.transform.position, _playerShootingPoint.transform.rotation);

    }
    
    public void OnRobotShoot()
    {
        IEnumerator coroutine = CoolDown(0.5f);    
        if(_robotShootCoolDown)
        {
            IAttack playerAttack = _energyBullet.GetComponent<IAttack>();
      
            // shoot to loss energy 
            EnergyGain(1);
            playerAttack.Damage = 1;
        
            // player bullet material change;
            Instantiate(_robotBullet, _robotShootingPoint.transform.position, _robotShootingPoint.transform.rotation);
            
            // Reload element ui
            _playerEvents.PlayerSaveValue(_energySaveValue);
            
            _robotShootCoolDown = false;
            StartCoroutine(coroutine);
        }
    
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

        ElementReset();
    }

    private void ElementReset()
    {
        if (_energySaveValue <= 0)
        {
            _savedElement = Elements.none;
            _playerEvents.PlayerSaveElement(_savedElement);
        }
        
    }


    private IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        _robotShootCoolDown = true;     
    }
}
