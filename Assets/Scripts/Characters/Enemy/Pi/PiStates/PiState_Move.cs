using UnityEngine;

public class PiState_Move : IPiState
{
    public override void EnterState()
    {
        _animator.Play(_data.MoveAnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {  
        if(_enemy.IsHurt)
        {
            _stateMachine.SetState(typeof(PiState_Hurt));
        }    
        else if(_enemy.PlayerDistance < 15f)
        {        
            _stateMachine.SetState(typeof(PiState_Idle));
        }

    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
        
        if(_enemy.PlayerDistance > 20f)
        {      
            Vector3 forward = _enemy.PlayerPosition * _data.ForwardSpeed;
            _enemy.SetVelocityXZ(forward);
        }
    }
}
