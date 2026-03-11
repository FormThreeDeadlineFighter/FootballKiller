using UnityEngine;

public class BaseballState_Die : IBaseballState
{
    BaseballStateConfig_Die _data;

    public BaseballState_Die(BaseballStateConfig_Die data) : base(data)
    {
        _data = data;
    }
    
    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
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
