using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/SectorShoot", fileName = "ShooterStateConfig_SectorShoot")]
public class ShooterStateConfig_SectorShoot : ScriptableObject, IStateConfig
{
    public TimelineAsset Timeline;
}
