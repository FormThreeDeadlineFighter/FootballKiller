using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnergyController : MonoBehaviour
{
    [SerializeField] private float _energySaver;
    [SerializeField] private Elements _savedElement;
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _shootingPoint;
    [SerializeField] GameObject _energyBullet;
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
            Debug.Log("player save energy");
            _energySaver += 10f;
            return true;
            
        }
        else
        {
            Debug.Log("player save fail");
            PlayerEvents.current.OnPLayerHurt(_playerBlockDetector.AttackDamage);
            return false;     
        }        
    }
    
    public void OnShoot()
    {
        if(_energySaver > 10f)
        {
            _energySaver -= 10f;
            Instantiate(_energyBullet, _shootingPoint.transform.position,  _shootingPoint.transform.rotation);
        }
    }
    
}
