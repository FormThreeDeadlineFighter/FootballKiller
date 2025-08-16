using UnityEngine;

public class BlockController : MonoBehaviour
{
    private float _energySaver;
    private Elements _detectElement => _playerBlockDetector._elementsBlock;
    private Elements _savedElement;
    PlayerBlockDetector _playerBlockDetector;
    void Awake()
    {
        _playerBlockDetector = GetComponentInChildren<PlayerBlockDetector>();
    }
    void OnEnable()
    {
        
    }
    
    void OnBlock()
    {
        if(_savedElement != Elements.none && _savedElement != _detectElement)
        {
            //player hurt
        }
        else
        {
            _energySaver += 10f;
            _savedElement = _detectElement;
        }
    }
}
