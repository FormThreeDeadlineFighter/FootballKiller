using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/InAir", fileName = "PlayerState_InAir")]
[System.Serializable]
public class PlayerState_InAir : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player in air");
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        _controller.SetState(typeof(PlayerState_Landing));
    }
    public override void PhysicsUpdate()
    { 

    }
}
