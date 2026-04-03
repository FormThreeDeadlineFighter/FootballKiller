using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig/Die", fileName = "BaseballStateConfig_Die")]
public class BaseballStateConfig_Die : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
