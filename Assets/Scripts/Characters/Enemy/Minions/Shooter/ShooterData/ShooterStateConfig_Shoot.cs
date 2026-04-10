using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Shoot", fileName = "ShooterStateConfig_Shoot")]
public class ShooterStateConfig_Shoot : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
