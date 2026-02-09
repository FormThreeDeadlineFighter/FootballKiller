using UnityEngine;

public class BaseballState_Wave : IBaseballState
{
    BaseballStateConfig_Wave _data;

    public BaseballState_Wave(BaseballStateConfig_Wave data) : base(data)
    {
        _data = data;
    }
    
    public override void EnterState()
    {
        
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
                
        _stateMachine.SetState(typeof(BaseballState_Idle));         
        
    }
    public override void PhysicsUpdate()
    {
        
    }
}
