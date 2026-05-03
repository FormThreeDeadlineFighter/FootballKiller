using UnityEngine;

public class ShooterState_Die : IShooterState
{
    public override void EnterState()
    {
        _animator.Play(_data.DieAnimationName);
        _enemy.IsTrackPlayer();
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {   
        if (IsAnimationComplete)
        {           
            _enemy.EnemyDie();         
        } 
    }
    public override void PhysicsUpdate()
    {
        
    }
}
