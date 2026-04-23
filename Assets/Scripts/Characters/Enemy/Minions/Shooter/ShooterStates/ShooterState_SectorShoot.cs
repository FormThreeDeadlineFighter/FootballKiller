using UnityEngine;
using UnityEngine.Playables;

public class ShooterState_SectorShoot : IShooter
{
    ShooterStateConfig_SectorShoot _data;

    public ShooterState_SectorShoot(ShooterStateConfig_SectorShoot data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _director.playableAsset = _data.Timeline;
        _director.time = 0;
        _director.Play();
        
        _enemy.NotTrackPlayer();
    }
    public override void ExitState()
    {
        _director.time = 0;
        //_director.Stop();
        
        _enemy.AttackCD();
    }
    public override void LogicUpdate()
    {   
        if(_director.state != PlayState.Playing)
        {
            _stateMachine.SetState(typeof(ShooterState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
