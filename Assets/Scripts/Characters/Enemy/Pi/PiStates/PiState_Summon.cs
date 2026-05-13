using UnityEngine;
using UnityEngine.Playables;

public class PiState_Summon : IPiState
{
    public override void EnterState()
    {
        _enemy.NotTrackPlayer();
        _director.playableAsset = _data.SummonTimeline;
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
        if(_enemy.IsDie)
        {
            _stateMachine.SetState(typeof(PiState_Die));
        }
        
        if(_director.state != PlayState.Playing)
        {
            _stateMachine.SetState(typeof(PiState_Idle)); 
        }

    }
    public override void PhysicsUpdate()
    {
        
    }
}
