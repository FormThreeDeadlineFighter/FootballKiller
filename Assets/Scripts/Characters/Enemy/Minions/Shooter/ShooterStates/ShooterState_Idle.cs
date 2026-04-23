using UnityEngine;

public class ShooterState_Idle : IShooter
{
    ShooterStateConfig_Idle _data;

    public ShooterState_Idle(ShooterStateConfig_Idle data) : base(data)
    {
        _data = data;
    }

    public override void EnterState()
    {
        _animator.Play(_data.AnimationName);
        Debug.Log("shooter idle");
    }
    public override void ExitState()
    {
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {   
        if(_enemy.CanAttack)
        {  
            int ran = Random.Range(1,3);
            if(ran == 1)
            {             
                _stateMachine.SetState(typeof(ShooterState_NormalShoot));   
            }
            if(ran == 2)
            {             
                _stateMachine.SetState(typeof(ShooterState_SectorShoot));   
            }
        }  
        else
        {
            //_stateMachine.SetState(typeof(ShooterState_Move));  
        }
    }
    public override void PhysicsUpdate()
    {
        _enemy.IsTrackPlayer();
    }
}
