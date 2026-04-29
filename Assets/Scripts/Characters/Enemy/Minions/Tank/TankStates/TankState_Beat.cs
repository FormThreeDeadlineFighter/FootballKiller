using UnityEngine;
using UnityEngine.Playables;

public class TankState_Beat : ITank
{
    public override void EnterState()
    {
        _director.playableAsset = _data.BeatTimeline;
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
            _stateMachine.SetState(typeof(TankState_Die));
        }
        
        if(_director.state != PlayState.Playing)
        {
            _stateMachine.SetState(typeof(TankState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
        if(_enemy.PlayerDistance > 5)
        {      
            Vector3 forward = _enemy.PlayerPosition * _data.ForwardSpeed;
            _enemy.SetVelocityXZ(forward);
        }
    }
}
