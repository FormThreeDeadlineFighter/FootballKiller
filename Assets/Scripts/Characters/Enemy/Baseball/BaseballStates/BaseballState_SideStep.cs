using UnityEngine;

public class BaseballState_SideStep : IBaseballState
{
    BaseballStateConfig_SideStep _data;

    public BaseballState_SideStep(BaseballStateConfig_SideStep data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.animationName);
        Debug.Log(_data.animationName);
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        
    }
    public override void PhysicsUpdate()
    {
        
    }
}
