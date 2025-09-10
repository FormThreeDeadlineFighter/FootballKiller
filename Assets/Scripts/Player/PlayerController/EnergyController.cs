using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnergyController : MonoBehaviour
{
    [SerializeField] private float _energySaver;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private Elements _savedElement;
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _shootingPoint;
    [SerializeField] GameObject _energyBullet;
    [SerializeField] PlayerEvents _playerEvents;
    private Elements _detectElement => _playerBlockDetector._elementsBlock;
    PlayerBlockDetector _playerBlockDetector;
    public bool IsBlock
    {
        get
        {
            if(_detectElement != Elements.none)
            {         
                return true;
            }
            return false;
        }
    }
    public bool IsSave => OnSave();
    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
    }
    void OnEnable()
    {
        _playerEvents.PlayerBlock(_energySaver);
    }
    void Update()
    {
        if(IsBlock)
        {
            OnSave();
        }
    }
    void OnBlock()
    {
        _blockDetector.SetActive(true);
    }
    bool OnSave()
    {
        if(_savedElement == Elements.none)
        {
            _savedElement = _detectElement; 
        }
        
        if(_detectElement == _savedElement)
        {
            if (_energySaver < _maxEnergy)
            {
                _energySaver += 10f;
                Debug.Log("player save energy");
            }
            else
            {
                Debug.Log("player energy full");
            }
            _playerEvents.PlayerBlock(_energySaver);
            return true;
            
        }
        else
        {
            Debug.Log("player save fail");
            _playerEvents.PlayerHurt(_playerBlockDetector.AttackDamage);
            return false;     
        }        
    }

    public void OnShoot()
    {
        if (_energySaver > 10f)
        {
            _energySaver -= 10f;
            Instantiate(_energyBullet, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
        }
        _playerEvents.PlayerBlock(_energySaver);
    }
    
}
