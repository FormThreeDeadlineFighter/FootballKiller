using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_BackJump", fileName = "BaseballStateConfig_BackJump")]
public class BaseballStateConfig_BackJump : ScriptableObject, IStateConfig
{
    public string AnimationName;
    public float BackJumpTriggerDistance;
    public float JumpBackForce;
    public float JumpUpForce;
}
