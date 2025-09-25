using UnityEngine;

public class PlayerState_Turn : IPlayerState
{
    [SerializeField] float _moveSpeed = 5.0f;
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if (_player.MoveMode == MoveMode.idle)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }
        if (_player.MoveMode == MoveMode.walk)
        {
            _stateMachine.SetState(typeof(PlayerState_Walk));
        }
        if (_input.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        if (_input.IsBlock && _player.CanBlock)
        {
            _stateMachine.SetState(typeof(PlayerState_Block));
        }
        if (_input.IsShoot && _player.CanShoot)
        {
            _stateMachine.SetState(typeof(PlayerState_Shoot));
        }
    }
    public override void PhysicsUpdate()
    {
        _player.Move(_moveSpeed);
    }
}
