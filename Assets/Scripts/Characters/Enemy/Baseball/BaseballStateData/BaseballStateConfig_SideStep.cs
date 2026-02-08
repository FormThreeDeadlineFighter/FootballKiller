using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_SideStep", fileName = "BaseballStateConfig_SideStep")]
public class BaseballStateConfig_SideStep : ScriptableObject, IStateConfig
{
    public string animationName;
}
