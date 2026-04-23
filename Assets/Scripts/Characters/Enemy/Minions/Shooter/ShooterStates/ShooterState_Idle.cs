using UnityEngine;

public class ShooterState_Idle : IShooter
{
    ShooterStateConfig_Idle _data;

    public ShooterState_Idle(ShooterStateConfig_Idle data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
        Debug.Log("shooter idle");
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(_enemy.CanAttack)
        {  
            _stateMachine.SetState(typeof(ShooterState_Shoot));   
        }  
        else
        {
            //_stateMachine.SetState(typeof(ShooterState_Move));  
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
