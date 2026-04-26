using System;
using UnityEngine;

public class BaseballState_Idle : IBaseballState
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
            _stateMachine.SetState(typeof(BaseballState_Hurt));
        }
        else if(_enemy.CanAttack)
        {  
            _stateMachine.SetState(typeof(BaseballState_PreAttack));   
        }  
        else
        {       
            _stateMachine.SetState(typeof(BaseballState_SideStep));        
        }
        
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
