using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private float _energySaver;
    [SerializeField] private Elements _savedElement;
    [SerializeField] GameObject _blockDetector;
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

    [System.Obsolete]
    void Update()
    {
        if(_blockDetector.activeInHierarchy && IsBlock)
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
            _energySaver += 10f;
            Debug.Log("player save energy");
            return true;
            
        }
        else
        {
            Debug.Log("player save fail");
            return false;     
        }        
    }
    
}
