using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
[System.Serializable]
public class PlayerState_Idle : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player idle");
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_playerInput.MoveMode == MoveMode.walk)
        {
            _stateMachine.SetState(typeof(PlayerState_Walk));
        }
        if(_playerInput.MoveMode == MoveMode.run)
        {
            _stateMachine.SetState(typeof(PlayerState_Run));
        }
        if(_playerInput.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
