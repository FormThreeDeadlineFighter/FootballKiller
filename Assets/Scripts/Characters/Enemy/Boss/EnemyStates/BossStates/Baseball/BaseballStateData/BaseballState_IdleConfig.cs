using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/IdleConfig", fileName = "BaseballState_IdleConfig")]
public class BaseballState_IdleConfig : ScriptableObject, IStateConfig
{
    public string animationName;
}
