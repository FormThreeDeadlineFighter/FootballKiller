using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_PreMove", fileName = "BaseballStateConfig_PreMove")]
public class BaseballStateConfig_PreMove : ScriptableObject, IStateConfig
{
    public string animationName;
}
