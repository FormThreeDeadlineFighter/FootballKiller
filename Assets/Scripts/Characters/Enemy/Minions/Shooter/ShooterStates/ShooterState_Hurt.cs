using UnityEngine;

public class ShooterState_Hurt : IShooterState
{
    public override void EnterState()
    {
        _animator.Play(_data.HurtAnimationName);
        _enemy.IsTrackPlayer();
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(ShooterState_Die));
        }
        
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(ShooterState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
