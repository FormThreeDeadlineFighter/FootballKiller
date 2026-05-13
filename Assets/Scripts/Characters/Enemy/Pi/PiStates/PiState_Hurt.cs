using UnityEngine;

public class PiState_Hurt : IPiState
{
    public override void EnterState()
    {
        _animator.Play(_data.HurtAnimationName);
        _enemy.IsTrackPlayer();
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {  
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(PiState_Die));
        }
        
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(PiState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
