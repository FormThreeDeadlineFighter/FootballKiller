using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/Pi", fileName = "StateConfig_Pi")]
public class StateConfig_Pi : ScriptableObject, IStateConfig
{
    [Header("Values")]
    public float ForwardSpeed;
    public float BackSpeed;
    public float ChargeSpeed;
    
    [Header("Animation")]
    public string IdleAnimationName;
    public string HurtAnimationName;
    public string DieAnimationName;
    
    [Header("TimelineAsset")]
    public TimelineAsset BeatTimeline;
    public TimelineAsset ChargeTimeline;
    public TimelineAsset ShootTimeline;
}
