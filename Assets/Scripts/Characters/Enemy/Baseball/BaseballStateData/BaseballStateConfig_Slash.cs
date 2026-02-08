using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_Slash", fileName = "BaseballStateConfig_Slash")]
public class BaseballStateConfig_Slash : ScriptableObject, IStateConfig
{
    public string animationName;
}
