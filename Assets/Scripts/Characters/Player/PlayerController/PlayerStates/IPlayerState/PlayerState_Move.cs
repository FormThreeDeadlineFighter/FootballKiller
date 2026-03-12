using Unity.Mathematics.Geometry;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : IPlayerState
{
    [SerializeField] float _moveSpeed = 7.0f;
    public override void EnterState()
    { 
        base.EnterState();
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(!_input.IsMove)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }
        if(_input.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        if (_input.IsBlock)
        {
            _stateMachine.SetState(typeof(PlayerState_Block));
        }
        if(_input.IsDash)
        {
            _stateMachine.SetState(typeof(PlayerState_Dash));
        }
    }
    public override void PhysicsUpdate()
    { 
        _player.Move(_moveSpeed);
    }
}
