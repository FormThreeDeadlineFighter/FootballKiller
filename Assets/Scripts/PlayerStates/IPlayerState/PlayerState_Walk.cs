using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
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
        if(_playerInput._playerMoveMode == MoveMode.idle)
        {
            _controller.SetState(typeof(PlayerState_Idle));
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
