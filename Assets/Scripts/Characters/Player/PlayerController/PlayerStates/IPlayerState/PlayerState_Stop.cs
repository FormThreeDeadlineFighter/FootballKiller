using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Stop", fileName = "PlayerState_Stop")]
public class PlayerState_Stop : IPlayerState
{
    public override void EnterState()
    { 
        
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {
        if(!_player.IsStop)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }
         
    }
    public override void PhysicsUpdate()
    { 

    }
}
