using System.ComponentModel.Design.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Landing", fileName = "PlayerState_Landing")]
public class PlayerState_Land : IPlayerState
{
    public override void EnterState()
    { 
        base.EnterState();
        _player.CanJump = true;
    }
    public override void ExitState()
    {
    
    }
    public override void LogicUpdate()
    {
        if(IsAnimationComplete)
        {
            if(!_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            } 
            if(_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Move));
            }
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
