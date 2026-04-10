using UnityEngine;

public class ShooterState_Shoot : IShooter
{
    ShooterStateConfig_Shoot _data;

    public ShooterState_Shoot(ShooterStateConfig_Shoot data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
        
        _enemy.AttackCD();
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(ShooterState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
