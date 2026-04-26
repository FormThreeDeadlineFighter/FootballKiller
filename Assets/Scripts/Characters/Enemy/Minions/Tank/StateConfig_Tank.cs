using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/Tank", fileName = "StateConfig_Tank")]
public class StateConfig_Tank : ScriptableObject, IStateConfig
{
    [Header("Values")]
    public float ForwardSpeed;
    public float BackSpeed;
    
    [Header("Animation")]
    public string IdleAnimationName;
    public string HurtAnimationName;
    public string DieAnimationName;
    
    [Header("TimelineAsset")]
    public TimelineAsset BeatTimeline;
    public TimelineAsset ChargeTimeline;
    public TimelineAsset ShootTimeline;
}
