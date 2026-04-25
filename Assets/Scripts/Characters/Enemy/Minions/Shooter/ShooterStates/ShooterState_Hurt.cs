using UnityEngine;

public class ShooterState_Hurt : IShooter
{
    ShooterStateConfig_Hurt _data;

    public ShooterState_Hurt(ShooterStateConfig_Hurt data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
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
