using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Landing", fileName = "PlayerState_Landing")]
[System.Serializable]
public class PlayerState_Landing : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player landing");
    }
    public override void ExitState()
    {
        _playerInput.IsJump = false;
    }
    public override void LogicUpdate()
    {
        _controller.SetState(typeof(PlayerState_Idle));
    }
    public override void PhysicsUpdate()
    { 

    }
}
