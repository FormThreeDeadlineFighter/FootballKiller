using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas UI;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _lossUI;
    [SerializeField] private GameEvent _gameEvent;

    void OnEnable()
    {
        _gameEvent.OnGameVictory += OnVectory;
        _gameEvent.OnGameDefeat += OnDefeat;
    }
    void OnDisable()
    {
        _gameEvent.OnGameVictory -= OnVectory;
        _gameEvent.OnGameDefeat -= OnDefeat;
    }

    void OnVectory()
    {
        Instantiate(_winUI, UI.transform);
        Debug.Log("You Win");
    }

    void OnDefeat()
    {
        Instantiate(_lossUI, UI.transform);
        Debug.Log("You Lose");
    }
}
