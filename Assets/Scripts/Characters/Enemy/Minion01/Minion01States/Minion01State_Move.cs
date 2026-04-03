using UnityEngine;

public class Minion01State_Move : IMinion01State
{
    Minion01StateConfig_Move _data;

    public Minion01State_Move(Minion01StateConfig_Move data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(_enemy.CanAttack)
        {
            _stateMachine.SetState(typeof(Minion01State_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}