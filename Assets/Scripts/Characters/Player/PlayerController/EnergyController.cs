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
    [SerializeField] private float kickForce = 12f;        // 球被踢出的力量     
    [SerializeField] private float kickRadius = 1.5f;    // 踢球偵測範圍
    
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
    public bool CanRetrieve => _currentEnergy >= 10;

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
    }
    
    // player press shoot
    /*public void OnPlayerShoot()
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
        
        // player shoot
        Vector3 dir = _sensor.Target.transform.position - _ballPosition.transform.position;
        Quaternion rotate = Quaternion.LookRotation(dir);
        GameObject ball = Instantiate(_ball, _ballPosition.transform.position, rotate);
        
        IAttack playerAttack = _ball.GetComponent<IAttack>();      
        playerAttack.Damage = _currentEnergy;
        
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.AddForce(dir * kickForce, ForceMode.Impulse);
            
        EnergyUse(_currentEnergy); // shoot to loss energy 

        _playerEvents.PlayerSaveValue(_currentEnergy); // Reload element ui
    }*/
    
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
        
        // ball detect
        Collider[] hits = new Collider[50];
        LayerMask layer = LayerMask.GetMask("Ball");
        hits = Physics.OverlapSphere(transform.position, kickRadius, layer);
        if(hits.Length <= 0) return;  
        Collider hit = hits[0];  
        
        if (!hit.CompareTag("Ball")) return;
        
        // player shoot
        Vector3 dir = target - transform.position;
        
        IAttack playerAttack = _ball.GetComponent<IAttack>();      
        playerAttack.Damage = _currentEnergy;
        
        Rigidbody ballRb = hit.GetComponent<Rigidbody>();
        ballRb.AddForce(dir * kickForce, ForceMode.Impulse);
            
        EnergyUse(_currentEnergy); // shoot to loss energy 
    }
    
    public void OnRobotShoot()
    {
        if (!_robotShootCoolDown) return;
        if (_sensor.Target == null) return;

        IEnumerator cd = CoolDown(0.5f);

        IAttack playerAttack = _robotBullet.GetComponent<IAttack>();
    
        // shoot damage
        playerAttack.Damage = 2;

        // player shoot
        Vector3 dir = _sensor.Target.transform.position - _robotShootingPoint.transform.position;
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
