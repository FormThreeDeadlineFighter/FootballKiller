using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Die", fileName = "ShooterStateConfig_Die")]
public class ShooterStateConfig_Die  : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
