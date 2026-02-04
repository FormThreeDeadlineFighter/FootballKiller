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
    
    public Action<GameObject> OnEnemyDestory;
    public void EnemyDestory(GameObject gameObject)
    {
        if (OnEnemyDestory != null)
        {
            OnEnemyDestory(gameObject);
        }
    }
}
