using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : IPlayerState
{
    [SerializeField] TimelineAsset _timeline;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _invincibleTime = 0.1f;
    public override void EnterState()
    { 
        _director.playableAsset = _timeline;
        _director.time = 0;
        _director.Play();
        
        _player.Jump(_jumpForce);
        _player.CanJump = false;
        
        _player.InvincibleStart(_invincibleTime);
    }
    public override void ExitState()
    {
        _director.time = 0;
        _director.Stop();
    }
    public override void LogicUpdate()
    {
        if(_player.IsFalling && _director.state != PlayState.Playing)
        {
            _stateMachine.SetState(typeof(PlayerState_Fall));
        }  
                     
    }
    public override void PhysicsUpdate()
    {
        _player.Move(_moveSpeed);
    }
}
