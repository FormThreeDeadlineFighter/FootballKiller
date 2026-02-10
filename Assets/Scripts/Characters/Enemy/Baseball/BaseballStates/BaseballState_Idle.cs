using System;
using UnityEngine;

public class BaseballState_Idle : IBaseballState
{
    BaseballStateConfig_Idle _data;

    public BaseballState_Idle(BaseballStateConfig_Idle data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {      
        if(_enemy.CanAttack)
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
        
    }
}
