using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig/Collision", fileName = "BaseballStateConfig_Collision")]
public class BaseballStateConfig_Collision : ScriptableObject, IStateConfig
{
    public TimelineAsset Timeline;
    public float ForwardForce;
}
