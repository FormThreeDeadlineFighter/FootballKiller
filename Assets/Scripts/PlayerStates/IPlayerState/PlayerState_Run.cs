using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
[System.Serializable]
public class PlayerState_Run : IPlayerState
{
    public override void EnterState()
    { 
        base.EnterState();
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_playerInput.MoveMode == MoveMode.idle)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }
        if(_playerInput.MoveMode == MoveMode.walk)
        {
            _stateMachine.SetState(typeof(PlayerState_Walk));
        }
        if(_playerInput.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        if(_playerInput.IsBlock)
        {
            _stateMachine.SetState(typeof(PlayerState_Block));
        }
        if(_playerInput.IsShoot)
        {
            _stateMachine.SetState(typeof(PlayerState_Shoot));
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
