using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/Shoot", fileName = "ShooterStateConfig_Shoot")]
public class ShooterStateConfig_Shoot : ScriptableObject, IStateConfig
{
    public TimelineAsset Timeline;
}
