using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/Pi", fileName = "StateConfig_Pi")]
public class StateConfig_Pi : ScriptableObject, IStateConfig
{
    [Header("Values")]
    public float ForwardSpeed;
    public float BackSpeed;
    
    [Header("Animation")]
    public string IdleAnimationName;
    public string MoveAnimationName;
    public string HurtAnimationName;
    public string DieAnimationName;
    
    [Header("TimelineAsset")]
    public TimelineAsset HeavyAttackTimeline;
    public TimelineAsset RoundAttackTimeline;
    public TimelineAsset SummonTimeline;
}
