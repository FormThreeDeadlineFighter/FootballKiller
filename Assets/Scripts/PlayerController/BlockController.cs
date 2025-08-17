using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private float _energySaver;
    [SerializeField] private Elements _savedElement;
    private Elements _detectElement => _playerBlockDetector._elementsBlock;
    PlayerBlockDetector _playerBlockDetector;
    private bool IsSave
    {
        get
        {
            if(_playerBlockDetector._elementsBlock != Elements.none)
            {      
                return true;
            }
            return false;
        }
    }
    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
    }
    void OnEnable()
    {
        
    }
    void Update()
    {
        if(IsSave)
        {
            _energySaver += 10f;
            _savedElement = _detectElement; 
        }
    }

    void OnSave()
    {
     
    }
}
