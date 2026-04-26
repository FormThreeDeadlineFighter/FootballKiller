using UnityEngine;

public class BaseballState_Hurt : IBaseballState
{
    
    public override void EnterState()
    {
        _enemy.IsTrackPlayer();
        _animator.Play(_data.HurtAnimationName);
    }
    public override void ExitState()
    {
       _enemy.IsHurt = false;
    }
    public override void LogicUpdate()
    { 
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(BaseballState_Die));
        }        
        if (IsAnimationComplete)
        {           
            _stateMachine.SetState(typeof(BaseballState_Idle));         
        }               
    }
    public override void PhysicsUpdate()
    {
        
    }
}
