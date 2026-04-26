using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/Baseball", fileName = "StateConfig_Baseball")]
public class StateConfig_Baseball : ScriptableObject, IStateConfig
{
    [Header("Values")]
    public float ForwardSpeed;
    public float ForwardTriggerDistance;
    public float BackJumpTriggerDistance;
    public float JumpBackForce;
    public float JumpUpForce;
    
    [Header("Animation")]
    public string IdleAnimationName;
    public string SideStepAnimationName;
    public string ForwardAnimationName;
    public string BackJumpAnimationName;
    public string HurtAnimationName;
    public string DieAnimationName;
    
    [Header("TimelineAsset")]
    public TimelineAsset CollisionTimeline;
    public TimelineAsset SlashTimeline;
    public TimelineAsset WaveTimeline;
}
