using UnityEngine;
using UnityEngine.Playables;

public class TankState_Charge : ITank
{
    public override void EnterState()
    {
         _director.playableAsset = _data.ChargeTimeline;
        _director.time = 0;
        _director.Play();
        _enemy.NotTrackPlayer();
    }
    public override void ExitState()
    {
         _director.time = 0;
        
        _enemy.AttackCD();
        _enemy.NotTrackPlayer();
    }
    public override void LogicUpdate()
    {  
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(TankState_Die));
        }
        
        if(_director.state != PlayState.Playing)
        {
            _stateMachine.SetState(typeof(TankState_Idle)); 
        }
    }
    public override void PhysicsUpdate()
    {
        if(_stateMachine.IsCharge)
        {      
            Vector3 forward = _enemy.transform.forward * _data.ChargeSpeed;
            _enemy.SetVelocityXZ(forward);
        }   
    }
}
