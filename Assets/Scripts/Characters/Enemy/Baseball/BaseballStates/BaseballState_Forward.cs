using UnityEngine;

public class BaseballState_Forward : IBaseballState
{
    BaseballStateConfig_Forward _data;

    public BaseballState_Forward(BaseballStateConfig_Forward data) : base(data)
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
        if(_enemy.PlayerDistance < _data.ForwardDistance - 2)
        {        
            _stateMachine.SetState(typeof(BaseballState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.FaceToPlayer();
    }
}
