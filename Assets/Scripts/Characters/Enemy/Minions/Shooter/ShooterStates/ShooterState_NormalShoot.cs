using UnityEngine;
using UnityEngine.Playables;

public class ShooterState_NormalShoot : IShooter
{
    ShooterStateConfig_NormalShoot _data;

    public ShooterState_NormalShoot(ShooterStateConfig_NormalShoot data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _director.playableAsset = _data.Timeline;
        _director.time = 0;
        _director.Play();
    }
    public override void ExitState()
    {
        _director.time = 0;
        //_director.Stop();
        
        _enemy.AttackCD();
        _enemy.NotTrackPlayer();
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
        _enemy.IsTrackPlayer();
    }
}
