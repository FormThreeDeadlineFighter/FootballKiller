using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Falling", fileName = "PlayerState_Falling")]
public class PlayerState_Fall : IPlayerState
{
    [SerializeField] AnimationCurve _speedCurve;
    [SerializeField] float _moveSpeed = 5f;
    public override void EnterState()
    { 
        base.EnterState();
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {
        if(_player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Land));
        }   
    }
    public override void PhysicsUpdate()
    {
        _player.SetVelocityY(_speedCurve.Evaluate(_stateDuration));
        _player.Move(_moveSpeed);
    }
}
