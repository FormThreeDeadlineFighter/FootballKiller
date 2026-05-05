using UnityEngine;

public class TankState_Hurt : ITankState
{
    public override void EnterState()
    {
        _enemy.IsTrackPlayer();
        _animator.Play(_data.HurtAnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {    
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(TankState_Die));
        }
        
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(TankState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
