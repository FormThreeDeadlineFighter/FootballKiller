using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Level _level;
    [SerializeField] Wave _currentWave;
    [SerializeField] int _currentWaveIndex;
    GameObject[] _currentEnemys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void GenerateEnemys()
    {
        _currentEnemys = _currentWave.Enemys;
        
        foreach(GameObject enemy in _currentEnemys)
        {
            Instantiate(enemy);
        }
    }
}
