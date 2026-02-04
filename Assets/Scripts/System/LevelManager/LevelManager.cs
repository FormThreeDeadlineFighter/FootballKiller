using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level _level;
    [SerializeField] Transform _gameLevel;
    [SerializeField] GameEvent gameEvent;
    GameObject _currentWave;
    List<GameObject> _currentEnemys;
    int _currentEnemysIndex;
    int _currentWaveIndex;
    void OnEnable()
    {
        gameEvent.OnEnemyDestory += OnEnemyDestory;
        
        _currentWaveIndex = 0;
        _currentEnemysIndex = 0;
    }

    void OnDisable()
    {  
        gameEvent.OnEnemyDestory -= OnEnemyDestory;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentEnemysIndex == 0 && _currentWaveIndex < _level.waves.Length)
        {
            GenerateWave();
            _currentWaveIndex++;      
        }
        if(_currentEnemysIndex == 0 && _currentWaveIndex == _level.waves.Length)
        {
            gameEvent.GameVictory();
        }
    }
    
    void GenerateWave()
    {          
        _currentWave = Instantiate(_level.waves[_currentWaveIndex], _gameLevel); 
        Wave wave = _currentWave.GetComponent<Wave>();
        _currentEnemysIndex = wave.enemys.Length; 
    }
    
    void OnEnemyDestory()
    {
        _currentEnemysIndex -= 1;
        
        if(_currentEnemysIndex == 0)
        {
            Destroy(_currentWave.gameObject);
        }
    }
}
