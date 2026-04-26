using UnityEngine;

public class BaseballState_Forward : IBaseballState
{
    public override void EnterState()
    {
        _animator.Play(_data.ForwardAnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    
    public override void LogicUpdate()
    { 
        if(_enemy.IsHurt)
        {
            _stateMachine.SetState(typeof(BaseballState_Hurt));
        }    
        else if(_enemy.PlayerDistance < _data.ForwardTriggerDistance - 2)
        {        
            _stateMachine.SetState(typeof(BaseballState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
        Vector3 forward = _enemy.PlayerPosition * _data.ForwardSpeed;
        _enemy.SetVelocityXZ(forward);
    }
}
