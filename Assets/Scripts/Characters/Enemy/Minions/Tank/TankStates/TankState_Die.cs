using UnityEngine;

public class TankState_Die : ITank
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