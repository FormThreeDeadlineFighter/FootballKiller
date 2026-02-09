using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig_Wave", fileName = "BaseballStateConfig_Wave")]
public class BaseballStateConfig_Wave : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
