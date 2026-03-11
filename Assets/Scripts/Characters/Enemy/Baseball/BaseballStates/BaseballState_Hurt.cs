using UnityEngine;

public class BaseballState_Hurt : IBaseballState
{
    BaseballStateConfig_Hurt _data;

    public BaseballState_Hurt(BaseballStateConfig_Hurt data) : base(data)
    {
        _data = data;
    }
    
    public override void EnterState()
    {
        _enemy.FaceToPlayer();
        _animator.Play(_data.AnimationName);
    }
    public override void ExitState()
    {
       _enemy.IsHurt = false;
    }
    public override void LogicUpdate()
    {         
        if (IsAnimationComplete)
        {           
            _stateMachine.SetState(typeof(BaseballState_Idle));         
        }               
    }
    public override void PhysicsUpdate()
    {
        
    }
}
