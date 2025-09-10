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
    PlayerBlockDetector _playerBlockDetector;

    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
    }
    void OnEnable()
    {
        _playerEvents.PlayerSave(_energySaver);
        _playerEvents.OnPlayerBlock += OnSave;
    }

    void OnSave(Elements element)
    {
        if(_savedElement == Elements.none)
        {
            Debug.Log($"detect {element}");
            _savedElement = element; 
        }
        
        if(element != _savedElement)
        {      
            Debug.Log("player save fail");
            _playerEvents.PlayerHurt(_playerBlockDetector.AttackDamage); 
        }
        else
        {
            if (_energySaver < _maxEnergy)
            {
                _energySaver += _playerBlockDetector.AttackDamage;
                Debug.Log("player save energy");
            }
            else
            {
                Debug.Log("player energy full");
            }

            _playerEvents.PlayerSave(_energySaver);
        }        
    }

    public void OnShoot()
    {
        if (_energySaver > 0f)
        {
            _energySaver -= 10f;
            Instantiate(_energyBullet, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
        }
        _playerEvents.PlayerSave(_energySaver);

        if (_energySaver == 0)
        {
            _savedElement = Elements.none;
        }
    }
    
}
