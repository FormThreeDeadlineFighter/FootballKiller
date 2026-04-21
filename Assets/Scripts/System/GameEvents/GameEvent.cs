using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannels/GameEvent", fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public Action OnGameVictory;
    public void GameVictory()
    {
        if (OnGameVictory != null)
        {
            OnGameVictory();
        }
    }

    public Action OnGameDefeat;
    public void GameDefeat()
    {
        if (OnGameDefeat != null)
        {
            OnGameDefeat();
        }
    }
    
    public Action OnGamePause;
    public void GamePause()
    {
        if (OnGamePause != null)
        {
            OnGamePause();
        }
    }
    
    public Action OnEnemyDestory;
    public void EnemyDestory()
    {
        if (OnEnemyDestory != null)
        {
            OnEnemyDestory();
        }
    }
}
