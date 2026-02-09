using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_Idle", fileName = "BaseballStateConfig_Idle")]
public class BaseballStateConfig_Idle : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
