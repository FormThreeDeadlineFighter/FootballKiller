using UnityEngine;

public class Minion01State_Idle : IMinion01
{
    Minion01StateConfig_Idle _data;

    public Minion01State_Idle(Minion01StateConfig_Idle data) : base(data)
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
        
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
