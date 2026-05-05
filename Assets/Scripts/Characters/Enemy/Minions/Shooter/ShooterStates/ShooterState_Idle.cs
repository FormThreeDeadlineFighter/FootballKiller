using UnityEngine;

public class ShooterState_Idle : IShooterState
{
    public override void EnterState()
    {
        _animator.Play(_data.IdleAnimationName);
        Debug.Log("shooter idle");
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {  
        if(_enemy.IsHurt)
        {
            _stateMachine.SetState(typeof(ShooterState_Hurt));
        }
        
        if(_enemy.CanAttack)
        {           
            _stateMachine.SetState(typeof(ShooterState_PreAttack));
        }  

    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
        
        if(_enemy.PlayerDistance > 20f)
        {      
            Vector3 forward = _enemy.PlayerPosition * _data.ForwardSpeed;
            _enemy.SetVelocityXZ(forward);
        }
    }
}
