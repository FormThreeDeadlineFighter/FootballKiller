using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Idle", fileName = "ShooterStateConfig_Idle")]
public class ShooterStateConfig_Idle : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
