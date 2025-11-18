using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(AISensor))]
public class EnergyController : MonoBehaviour
{
    private float _energySaveValue;
    [Header("Setting")]
    [SerializeField] private float _maxEnergy;
    [SerializeField] private Elements _savedElement;
    [SerializeField] private float kickForce = 12f;        // 球被踢出的力量
    [SerializeField] private float kickRadius = 1.2f;      // 踢球偵測範圍
    
    [Header("Objects")]
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _robotShootingPoint;
    [SerializeField] GameObject _robotBullet;
    [SerializeField] PlayerEvent _playerEvents;

    private PlayerBlockDetector _playerBlockDetector;
    private AISensor _sensor;

    public bool CanBlock;
    public bool CanShoot => _energySaveValue >= 0;

    private bool _robotShootCoolDown = true;

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
        if (_sensor.Target == null) return;
        
        //shoot damage and energy change
        //IAttack playerAttack = _energyBullet.GetComponent<IAttack>();
        //playerAttack.Elements = _savedElement;
        //playerAttack.Damage = _energySaveValue;

        // shoot to loss energy 
        EnergyUse(_energySaveValue);

        // ball detect
        Vector3 center = transform.position + transform.forward * 1f;
        Collider[] hits = Physics.OverlapSphere(center, kickRadius);
        if(hits.Length <= 0) return;  
        Collider hit = hits[0];   
        
        if (!hit.CompareTag("Ball")) return;
        Rigidbody ballRb = hit.GetComponent<Rigidbody>(); 
        Vector3 dir = (_sensor.Target.transform.position - transform.position).normalized;
        ballRb.AddForce(dir * kickForce, ForceMode.Impulse);

        Debug.Log("Kicked Ball!");  
    }
    
    public void OnRobotShoot()
    {
        if (!_robotShootCoolDown) return;
        if (_sensor.Target == null) return;

        IEnumerator coroutine = CoolDown(0.5f);

        IAttack playerAttack = _robotBullet.GetComponent<IAttack>();
    
        // shoot to loss energy 
        EnergyGain(1);
        playerAttack.Damage = 1;

        // player bullet material change;
        Vector3 dir = _sensor.Target.transform.position - _robotShootingPoint.transform.position;
        Quaternion rotate = Quaternion.LookRotation(dir);
        Instantiate(_robotBullet, _robotShootingPoint.transform.position, rotate);
        
        // Reload element ui
        _playerEvents.PlayerSaveValue(_energySaveValue);
        
        _robotShootCoolDown = false;
        StartCoroutine(coroutine);
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
