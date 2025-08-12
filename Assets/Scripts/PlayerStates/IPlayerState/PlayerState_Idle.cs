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
        if(_playerInput._playerMoveMode == MoveMode.walk)
        {
            _controller.SetState(typeof(PlayerState_Walk));
        }
        if(_playerInput._playerMoveMode == MoveMode.run)
        {
            _controller.SetState(typeof(PlayerState_Run));
        }
        if(_playerInput.IsJump)
        {
            _controller.SetState(typeof(PlayerState_Jump));
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
