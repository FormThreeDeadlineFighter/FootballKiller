using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Move", fileName = "ShooterStateConfig_Move")]
public class ShooterStateConfig_Move : ScriptableObject, IStateConfig
{
    public string AnimationName;
}
