using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(AISensor))]
public class EnergyController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _maxEnergy;
    [SerializeField] private Elements _savedElement;
    [SerializeField] private float kickForce = 12f;        // 球被踢出的力量     // 踢球偵測範圍
    
    [Header("Objects")]
    [SerializeField] GameObject _blockDetector;
    [SerializeField] Transform _ballPosition;
    [SerializeField] GameObject _robotShootingPoint;
    [SerializeField] GameObject _ball;
    [SerializeField] GameObject _robotBullet;
    [SerializeField] PlayerEvent _playerEvents;

    private PlayerBlockDetector _playerBlockDetector;
    private AISensor _sensor;
    private float _currentEnergy;
    private bool _robotShootCoolDown = true; 

    public bool CanBlock;
    public bool CanShoot => _currentEnergy >= 0;

    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
        _sensor = GetComponent<AISensor>();
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
        
        _playerEvents.PlayerSaveValue(_currentEnergy);
    }
    
    // player press shoot
    public void OnPlayerShoot()
    {
        Vector3 target;

        if (_sensor.Target == null) 
        {
            target = _ballPosition.position;
        }
        else
        {
            target = _sensor.Target.transform.position;
        }

        IAttack playerAttack = _robotBullet.GetComponent<IAttack>();

        // shoot to loss energy 
        playerAttack.Damage = _currentEnergy;
        
        // player shoot
        Vector3 dir = _sensor.Target.transform.position - _robotShootingPoint.transform.position;
        Quaternion rotate = Quaternion.LookRotation(dir);
        GameObject ball = Instantiate(_ball, _ballPosition.transform.position, rotate);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.AddForce(dir * kickForce, ForceMode.Impulse);
            
        EnergyUse(_currentEnergy); // shoot to loss energy 

        _playerEvents.PlayerSaveValue(_currentEnergy); // Reload element ui
    }
    
    public void OnRobotShoot()
    {
        if (!_robotShootCoolDown) return;
        if (_sensor.Target == null) return;

        IEnumerator cd = CoolDown(0.5f);

        IAttack playerAttack = _robotBullet.GetComponent<IAttack>();
    
        // shoot to gain energy 
        EnergyGain(1);
        playerAttack.Damage = 1;

        // player shoot
        Vector3 dir = _sensor.Target.transform.position - _robotShootingPoint.transform.position;
        Quaternion rotate = Quaternion.LookRotation(dir);
        Instantiate(_robotBullet, _robotShootingPoint.transform.position, rotate);
             
        _playerEvents.PlayerSaveValue(_currentEnergy); // Reload element ui
        
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
        
        _playerEvents.PlayerSaveValue(_currentEnergy);
        _playerEvents.PlayerSaveElement(_savedElement);
    }

    public void EnergyUse(float value)
    {
        _currentEnergy -= value;
        
        if(_currentEnergy <= 0)
        {
            _currentEnergy = 0;
        }
        
        _playerEvents.PlayerSaveValue(_currentEnergy);
        _playerEvents.PlayerSaveElement(_savedElement);

        ElementReset();
    }

    private void ElementReset()
    {
        if (_currentEnergy <= 0)
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
