using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Falling", fileName = "PlayerState_Falling")]
public class PlayerState_Fall : IPlayerState
{
    [SerializeField] AnimationCurve _speedCurve;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _duration = 1.5f;
    private float _currentTime;
    public override void EnterState()
    { 
        base.EnterState();
        _currentTime = 0;
        
    }
    public override void ExitState()
    {
        _currentTime = 0;
    }
    public override void LogicUpdate()
    {
        if(_player.IsGrounded || _currentTime > _duration)
        {
            _stateMachine.SetState(typeof(PlayerState_Land));
        }   
        if(_currentTime < _duration)
        {    
            _currentTime += Time.deltaTime;
        }
        Debug.Log(_currentTime);
    }
    public override void PhysicsUpdate()
    {
        _player.SetVelocityY(_speedCurve.Evaluate(_stateDuration));
        _player.Move(_moveSpeed);
    }
}
