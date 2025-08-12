using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
[System.Serializable]
public class PlayerState_Run : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player run");
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_playerInput._playerMoveMode == MoveMode.idle)
        {
            _controller.SetState(typeof(PlayerState_Idle));
        }
        if(_playerInput._playerMoveMode == MoveMode.walk)
        {
            _controller.SetState(typeof(PlayerState_Walk));
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
