using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Block", fileName = "PlayerState_Block")]
[System.Serializable]
public class PlayerState_Block : IPlayerState
{
    public override void EnterState()
    { 
        Debug.Log("player block");
        
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
