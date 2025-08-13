using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
[System.Serializable]
public class PlayerState_Walk : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player walk");
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
