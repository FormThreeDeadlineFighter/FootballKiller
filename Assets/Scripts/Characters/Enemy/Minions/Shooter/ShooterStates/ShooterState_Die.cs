using UnityEngine;

public class ShooterState_Die : IShooter
{
    ShooterStateConfig_Die _data;

    public ShooterState_Die(ShooterStateConfig_Die data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
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
