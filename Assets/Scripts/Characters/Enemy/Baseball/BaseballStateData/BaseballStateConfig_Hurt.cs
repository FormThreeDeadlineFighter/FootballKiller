using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/BaseballStateConfig/Hurt", fileName = "BaseballStateConfig_Hurt")]
public class BaseballStateConfig_Hurt : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
