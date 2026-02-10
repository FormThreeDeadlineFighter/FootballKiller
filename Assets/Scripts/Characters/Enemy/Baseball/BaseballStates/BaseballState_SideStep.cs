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
        
        if(_enemy.PlayerDistance > _data.ForwardDistance)
        {
            _stateMachine.SetState(typeof(BaseballState_Forward));
        }
        else if (_enemy.PlayerDistance < _data.BackJumpDistance && _enemy.CanJump)
        {
            _stateMachine.SetState(typeof(BaseballState_BackJump));
        } 
    }
    public override void PhysicsUpdate()
    {
        _enemy.FaceToPlayer();
    }
}
