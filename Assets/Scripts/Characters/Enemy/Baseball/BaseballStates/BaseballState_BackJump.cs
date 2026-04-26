using UnityEngine;

public class BaseballState_BackJump : IBaseballState
{
    public override void EnterState()
    {
        _animator.Play(_data.BackJumpAnimationName);
        
        _enemy.IsTrackPlayer();
        Vector3 backJump = -_enemy.PlayerPosition * _data.JumpBackForce;
        _enemy.SetVelocityXZ(backJump);
        _enemy.SetVelocityY(_data.JumpUpForce);
        
    }
    public override void ExitState()
    {
        _enemy.JumpCD();
    }
    public override void LogicUpdate()
    {
        if(IsAnimationComplete)
        {        
            _stateMachine.SetState(typeof(BaseballState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
