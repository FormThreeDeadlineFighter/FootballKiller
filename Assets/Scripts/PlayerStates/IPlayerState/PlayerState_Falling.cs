using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Falling", fileName = "PlayerState_Falling")]
[System.Serializable]
public class PlayerState_Falling : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player falling");
        
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Landing));
        }   
    }
    public override void PhysicsUpdate()
    { 
        
    }
}
