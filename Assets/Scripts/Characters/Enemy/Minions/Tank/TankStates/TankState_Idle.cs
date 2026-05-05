using UnityEngine;

public class TankState_Idle : ITankState
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
            _stateMachine.SetState(typeof(TankState_Hurt));
        }
        
        if(_enemy.CanAttack)
        {           
            _stateMachine.SetState(typeof(TankState_PreAttack));
        }  

    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
