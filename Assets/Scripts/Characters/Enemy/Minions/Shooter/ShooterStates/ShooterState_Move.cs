using UnityEngine;

public class ShooterState_Move : IShooter
{
    ShooterStateConfig_Move _data;

    public ShooterState_Move(ShooterStateConfig_Move data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(_enemy.CanAttack)
        {
            _stateMachine.SetState(typeof(ShooterState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}