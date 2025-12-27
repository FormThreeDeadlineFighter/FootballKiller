using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnergyController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _maxEnergy;
    [SerializeField] private Elements _savedElement;
    [SerializeField] private float kickForce = 12f;        // 球被踢出的力量     
    [SerializeField] private float kickRadius = 1.5f;    // 踢球偵測範圍
    
    [Header("Objects")]
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _robotShootingPoint;
    [SerializeField] GameObject _robotBullet;
    [SerializeField] PlayerEvent _playerEvents;

    private PlayerBlockDetector _playerBlockDetector;
    private float _currentEnergy;
    private bool _robotShootCoolDown = true; 

    public bool CanBlock;
    public bool CanShoot => _currentEnergy >= 0;
    public bool CanRetrieve => _currentEnergy >= 10;

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
    }

    void OnBlock(Elements element)
    {
        if (_currentEnergy < _maxEnergy)
        {
            _savedElement = element;
            EnergyGain(_playerBlockDetector.AttackDamage * 0.5f);
            Debug.Log($"save {element} energy");
        }
        else
        {
            Debug.Log("energy full");
        }             
    }
    
    public void OnPlayerShoot(float value, bool noCost = false)
    {
        
    }
    
    public void OnRobotShoot(Vector3 target)
    {
        if (!_robotShootCoolDown) return;
        if (target == null) return;

        IEnumerator cd = CoolDown(0.5f);

        IAttack playerAttack = _robotBullet.GetComponent<IAttack>();
    
        // shoot damage
        playerAttack.Damage = 2;

        // player shoot
        Vector3 dir = target - _robotShootingPoint.transform.position;
        Quaternion rotate = Quaternion.LookRotation(dir);
        Instantiate(_robotBullet, _robotShootingPoint.transform.position, rotate);
             
        EnergyGain(2); // energy gain
        
        _robotShootCoolDown = false;
        StartCoroutine(cd);
    }
    
    

    public void EnergyGain(float value)
    {
        ElementReset();
        _currentEnergy += value;
      
        if(_currentEnergy > _maxEnergy)
        {
            _currentEnergy = _maxEnergy;
        }
        
        float _energyPercentage = _currentEnergy / _maxEnergy;
        _playerEvents.PlayerSaveValue(_energyPercentage);
        _playerEvents.PlayerSaveElement(_savedElement);
    }

    public void EnergyUse(float value)
    {
        _currentEnergy -= value;
        
        if(_currentEnergy <= 0)
        {
            _currentEnergy = 0;
        }
        
        float _energyPercentage = _currentEnergy / _maxEnergy;
        _playerEvents.PlayerSaveValue(_energyPercentage);
        _playerEvents.PlayerSaveElement(_savedElement);

        ElementReset();
    }

    private void ElementReset()
    {
        if (_currentEnergy <= 0)
        {
            _playerEvents.PlayerSaveElement(_savedElement);
        }
        
    }


    private IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        _robotShootCoolDown = true;     
    }
}
