using UnityEngine;

public class FlyGaltingState_Hurt : IFlyGaltingState
{
    public override void EnterState()
    {
        _enemy.IsTrackPlayer();
        _animator.Play(_data.IdleAnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {    
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(FlyGaltingState_Die));
        }
        
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(FlyGaltingState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
