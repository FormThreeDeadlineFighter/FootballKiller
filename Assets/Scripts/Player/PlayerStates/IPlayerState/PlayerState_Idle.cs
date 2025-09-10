using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
[System.Serializable]
public class PlayerState_Idle : IPlayerState
{
    public override void EnterState()
    {
        base.EnterState();
        _player.PlayerBlockExit();
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_player.MoveMode == MoveMode.walk)
        {
            _stateMachine.SetState(typeof(PlayerState_Walk));
        }
        if(_player.MoveMode == MoveMode.run)
        {
            _stateMachine.SetState(typeof(PlayerState_Run));
        }
        if(_input.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        if(_player.IsFalling)
        {
            _stateMachine.SetState(typeof(PlayerState_Fall));
        }       
        if(_input.IsBlock)
        {
            _stateMachine.SetState(typeof(PlayerState_Block));
        }
        if(_input.IsShoot)
        {
            _stateMachine.SetState(typeof(PlayerState_Shoot));
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
