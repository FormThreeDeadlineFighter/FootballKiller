using UnityEngine;

public class PiState_Idle : IPiState
{
    public override void EnterState()
    {
        _animator.Play(_data.IdleAnimationName);
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
        else if(_enemy.CanAttack)
        {  
            _stateMachine.SetState(typeof(PiState_PreAttack));   
        }  
        else if(_enemy.PlayerDistance > 20f)
        {        
            _stateMachine.SetState(typeof(PiState_Move));
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
