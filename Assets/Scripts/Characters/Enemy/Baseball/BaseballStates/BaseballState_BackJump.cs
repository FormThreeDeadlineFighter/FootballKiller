using UnityEngine;

public class BaseballState_BackJump : IBaseballState
{
    BaseballStateConfig_BackJump _data;

    public BaseballState_BackJump(BaseballStateConfig_BackJump data) : base(data)
    {
        _data = data;
    }
    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
        
        _enemy.FaceToPlayer();
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
