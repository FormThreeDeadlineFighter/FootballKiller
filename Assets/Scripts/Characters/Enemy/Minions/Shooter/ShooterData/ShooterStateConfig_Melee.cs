using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Melee", fileName = "ShooterStateConfig_Melee")]
public class ShooterStateConfig_Melee : ScriptableObject, IStateConfig
{
    public TimelineAsset Timeline;
}
