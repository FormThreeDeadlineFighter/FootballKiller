using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Hurt", fileName = "ShooterStateConfig_Hurt")]
public class ShooterStateConfig_Hurt  : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
