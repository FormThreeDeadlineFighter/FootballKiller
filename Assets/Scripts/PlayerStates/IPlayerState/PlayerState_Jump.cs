using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
[System.Serializable]
public class PlayerState_Jump : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player jump");
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        _controller.SetState(typeof(PlayerState_InAir));
    }
    public override void PhysicsUpdate()
    { 

    }
}
