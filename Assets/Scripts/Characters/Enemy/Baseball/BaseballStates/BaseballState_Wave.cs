using UnityEngine;
using UnityEngine.Playables;

public class BaseballState_Wave : IBaseballState
{
    BaseballStateConfig_Wave _data;

    public BaseballState_Wave(BaseballStateConfig_Wave data) : base(data)
    {
        _data = data;
    }
    
    public override void EnterState()
    {
        _enemy.FaceToPlayer();
        _director.playableAsset = _data.Timeline;
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
        if (_director.state != PlayState.Playing)
        {           
            _stateMachine.SetState(typeof(BaseballState_Idle));         
        }               
    }
    public override void PhysicsUpdate()
    {
        
    }
}
