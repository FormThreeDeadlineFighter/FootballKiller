using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig/Wave", fileName = "BaseballStateConfig_Wave")]
public class BaseballStateConfig_Wave : ScriptableObject, IStateConfig
{
    public TimelineAsset Timeline;
}
