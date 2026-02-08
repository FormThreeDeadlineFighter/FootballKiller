using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_Collision", fileName = "BaseballStateConfig_Collision")]
public class BaseballStateConfig_Collision : ScriptableObject, IStateConfig
{
    public string animationName;
}
