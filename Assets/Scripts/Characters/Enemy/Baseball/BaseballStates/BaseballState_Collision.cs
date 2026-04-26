using UnityEngine;
using UnityEngine.Playables;

public class BaseballState_Collision : IBaseballState
{
    float currentTime;

    public override void EnterState()
    {
        currentTime = 0;
        _director.playableAsset = _data.CollisionTimeline;
        _director.time = 0;
        _director.Play();
    }
    public override void ExitState()
    {
        _director.time = 0;
        _director.Stop();
        _enemy.AttackCD();
    }
    public override void LogicUpdate()
    {
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(BaseballState_Die));
        }  
        
        if (_director.state != PlayState.Playing)
        {           
            _stateMachine.SetState(typeof(BaseballState_Idle));         
        }  
    }
    public override void PhysicsUpdate()
    {   
                     
    }
}
