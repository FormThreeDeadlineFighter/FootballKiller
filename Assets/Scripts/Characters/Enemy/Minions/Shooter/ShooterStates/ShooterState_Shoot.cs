using UnityEngine;

public class ShooterState_Shoot : IShooter
{
    ShooterStateConfig_Shoot _data;

    public ShooterState_Shoot(ShooterStateConfig_Shoot data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _director.playableAsset = _data.Timeline;
        _director.time = 0;
        _director.Play();
            
        Debug.Log("shooter shoot");
    }
    public override void ExitState()
    {
        _director.time = 0;
        //_director.Stop();
        
        _enemy.AttackCD();
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(ShooterState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
