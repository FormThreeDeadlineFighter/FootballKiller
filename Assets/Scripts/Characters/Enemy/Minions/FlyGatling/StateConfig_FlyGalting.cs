using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/FlyGalting", fileName = "StateConfig_FlyGalting")]
public class StateConfig_FlyGalting : ScriptableObject, IStateConfig
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
}