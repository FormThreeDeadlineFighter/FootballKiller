using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_Forward", fileName = "BaseballStateConfig_Forward")]
public class BaseballStateConfig_Forward : ScriptableObject, IStateConfig
{
    public string AnimationName;
    public float ForwardTriggerDistance;
    public float ForwardForce;
}
