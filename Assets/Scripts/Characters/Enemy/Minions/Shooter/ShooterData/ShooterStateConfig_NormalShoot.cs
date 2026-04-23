using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyData/ShooterStateConfig/NormalShoot", fileName = "ShooterStateConfig_NormalShoot")]
public class ShooterStateConfig_NormalShoot : ScriptableObject, IStateConfig
{
    public TimelineAsset Timeline;
}
