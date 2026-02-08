using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_PreAttack", fileName = "BaseballStateConfig_PreAttack")]
public class BaseballStateConfig_PreAttack : ScriptableObject, IStateConfig
{
    public string animationName;
}
