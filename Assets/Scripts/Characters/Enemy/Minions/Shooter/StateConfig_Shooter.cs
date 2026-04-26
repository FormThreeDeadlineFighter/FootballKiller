using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/Shooter", fileName = "StateConfig_Shooter")]
public class StateConfig_Shooter : ScriptableObject, IStateConfig
{
    [Header("Values")]
    public float ForwardSpeed;
    public float BackSpeed;
    
    [Header("Animation")]
    public string IdleAnimationName;
    public string HurtAnimationName;
    public string DieAnimationName;
    
    [Header("TimelineAsset")]
    public TimelineAsset ShootTimeline;
    public TimelineAsset MeleeTimeline;
}
