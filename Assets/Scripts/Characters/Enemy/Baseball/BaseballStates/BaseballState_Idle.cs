using System;
using UnityEngine;

[Serializable]
public class BaseballState_Idle : IBaseballState
{
    BaseballState_IdleConfig _data;

    public BaseballState_Idle(BaseballState_IdleConfig data)
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
