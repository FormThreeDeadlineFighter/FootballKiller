using UnityEngine;

public class BaseballState_SideStep : IBaseballState
{
    public override void EnterState()
    {
        _animator.Play(_data.SideStepAnimationName);
    }
    public override void ExitState()
    {

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
        else if(_enemy.PlayerDistance > _data.ForwardTriggerDistance)
        {
            _stateMachine.SetState(typeof(BaseballState_Forward));
        }
        else if (_enemy.PlayerDistance < _data.BackJumpTriggerDistance && _enemy.CanBack)
        {
            _stateMachine.SetState(typeof(BaseballState_BackJump));
        } 
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
        Vector3 left = -_enemy.transform.right;
        _enemy.SetVelocityXZ(left);
    }
}
