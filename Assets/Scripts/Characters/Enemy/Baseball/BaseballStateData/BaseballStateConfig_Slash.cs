using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_Slash", fileName = "BaseballStateConfig_Slash")]
public class BaseballStateConfig_Slash : ScriptableObject, IStateConfig
{
    public string animationName;
    public TimelineAsset timeline;
}
