using UnityEngine;

public class FlyGaltingState_Idle : IFlyGaltingState
{
    public override void EnterState()
    {
        _animator.Play(_data.IdleAnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {  
        if(_enemy.IsHurt)
        {
            _stateMachine.SetState(typeof(FlyGaltingState_Hurt));
        }
        
        if(_enemy.CanAttack)
        {           
            _stateMachine.SetState(typeof(FlyGaltingState_PreAttack));
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
