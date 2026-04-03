using UnityEngine;

public class Minion01State_Shoot : IMinion01State
{
    Minion01StateConfig_Shoot _data;

    public Minion01State_Shoot(Minion01StateConfig_Shoot data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
        
        _enemy.AttackCD();
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(Minion01State_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
