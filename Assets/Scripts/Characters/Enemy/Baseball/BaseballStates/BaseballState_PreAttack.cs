using UnityEngine;

public class BaseballState_PreAttack : IBaseballState
{
    BaseballStateConfig_PreAttack _data;

    public BaseballState_PreAttack(BaseballStateConfig_PreAttack data) : base(data)
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
        _stateMachine.SetState(typeof(BaseballState_Slash)); 
    }
    public override void PhysicsUpdate()
    {
        
    }
}
