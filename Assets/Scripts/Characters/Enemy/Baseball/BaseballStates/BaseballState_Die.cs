using UnityEngine;

public class BaseballState_Die : IBaseballState
{ 
    public override void EnterState()
    {
        _animator.Play(_data.DieAnimationName);
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
