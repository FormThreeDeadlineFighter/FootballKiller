using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas UI;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _lossUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameEvent _gameEvent;
    private bool IsPause;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void OnEnable()
    {
        _gameEvent.OnGameVictory += OnVectory;
        _gameEvent.OnGameDefeat += OnDefeat;
        _gameEvent.OnGamePause += OnPause;
        
        IsPause = false;
    }
    void OnDisable()
    {
        _gameEvent.OnGameVictory -= OnVectory;
        _gameEvent.OnGameDefeat -= OnDefeat;
        _gameEvent.OnGamePause -= OnPause;
        
        IsPause = false;
    }

    void OnVectory()
    {
        Instantiate(_winUI, UI.transform);
        
        Debug.Log("You Win");
        Time.timeScale = 0f;
        _gameEvent.OnGameVictory -= OnVectory;
        _gameEvent.OnGameDefeat -= OnDefeat;
    }

    void OnDefeat()
    {
        Instantiate(_lossUI, UI.transform);
        Debug.Log("You Lose");
        Time.timeScale = 0f;
        _gameEvent.OnGameVictory -= OnVectory;
        _gameEvent.OnGameDefeat -= OnDefeat;
    }
    private void OnPause()
    {
        if(!IsPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
        _pauseUI.SetActive(true);  
        IsPause = true;
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
        _pauseUI.SetActive(false); 
        IsPause = false;
    }
}
