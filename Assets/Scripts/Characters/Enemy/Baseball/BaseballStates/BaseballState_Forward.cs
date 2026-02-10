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
        if(_enemy.CanAttack)
        {
            _stateMachine.SetState(typeof(BaseballState_Collision));
        }
        
        if(_enemy.PlayerDistance < _data.ForwardTriggerDistance - 2)
        {        
            _stateMachine.SetState(typeof(BaseballState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.FaceToPlayer();
        Vector3 forward = _enemy.PlayerPosition * _data.ForwardForce;
        _enemy.SetVelocityXZ(forward);
    }
}
