using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_SideStep", fileName = "BaseballStateConfig_SideStep")]
public class BaseballStateConfig_SideStep : ScriptableObject, IStateConfig
{
    [SerializeField] BaseballStateConfig_Forward forward;
    [SerializeField] BaseballStateConfig_BackJump backJump;
    public float ForwardDistance => forward.ForwardTriggerDistance;
    public float BackJumpDistance => backJump.BackJumpTriggerDistance;
    public string AnimationName; 
}
