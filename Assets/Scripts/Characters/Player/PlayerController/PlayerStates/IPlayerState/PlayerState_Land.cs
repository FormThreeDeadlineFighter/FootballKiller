using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Landing", fileName = "PlayerState_Landing")]
public class PlayerState_Land : IPlayerState
{
    [SerializeField] TimelineAsset _timeline;
    public override void EnterState()
    { 
        _director.playableAsset = _timeline;
        _director.time = 0;
        _director.Play();
        
        _player.CanJump = true;
    }
    public override void ExitState()
    {
        _director.time = 0;
        _director.Stop();
    }
    public override void LogicUpdate()
    {
        if(_director.state != PlayState.Playing)
        {
            if(!_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            } 
            if(_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Move));
            }
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
