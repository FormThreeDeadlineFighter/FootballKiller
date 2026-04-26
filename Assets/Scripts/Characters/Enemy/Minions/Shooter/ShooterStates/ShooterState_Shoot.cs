using UnityEngine;
using UnityEngine.Playables;

public class ShooterState_Shoot : IShooter
{
    public override void EnterState()
    {
        _director.playableAsset = _data.ShootTimeline;
        _director.time = 0;
        _director.Play();
    }
    public override void ExitState()
    {
        _director.time = 0;
        
        _enemy.AttackCD();
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {  
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(ShooterState_Die));
        }
        
        if(_director.state != PlayState.Playing)
        {
            _stateMachine.SetState(typeof(ShooterState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
        if(_enemy.PlayerDistance < 10)
        {      
            Vector3 back = -_enemy.PlayerPosition * _data.BackSpeed;
            _enemy.SetVelocityXZ(back);
        }
    }
}
