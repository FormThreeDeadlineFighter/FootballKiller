using UnityEngine;
using UnityEngine.Playables;

public class BaseballState_Collision : IBaseballState
{
    BaseballStateConfig_Collision _data;
    float currentTime;

    public BaseballState_Collision(BaseballStateConfig_Collision data) : base(data)
    {
        _data = data;
    }
    public override void EnterState()
    {
        currentTime = 0;
        _director.playableAsset = _data.Timeline;
        _director.time = 0;
        _director.Play();
    }
    public override void ExitState()
    {
        _director.time = 0;
        _enemy.AttackCD();
    }
    public override void LogicUpdate()
    {
        if (_director.state != PlayState.Playing)
        {           
            _stateMachine.SetState(typeof(BaseballState_Idle));         
        }  
    }
    public override void PhysicsUpdate()
    {   
        if(currentTime < 2)
        {         
            _enemy.FaceToPlayer();      
            currentTime += Time.fixedDeltaTime;
        }
        
    }
}
