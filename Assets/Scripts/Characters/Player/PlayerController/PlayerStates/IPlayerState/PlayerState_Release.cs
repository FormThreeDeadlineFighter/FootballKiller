using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Release", fileName = "PlayerState_Release")]
public class PlayerState_Release : IPlayerState
{
    public override void EnterState()
    { 
        
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {
        if(_player.CurrentHoldGrade == HoldGrade.level0)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack1));
        }
        if(_player.CurrentHoldGrade == HoldGrade.level1)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack2));
        }
        if(_player.CurrentHoldGrade == HoldGrade.level2)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack3));
        }     
        if(_player.CurrentHoldGrade == HoldGrade.level3)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack4));
        }   
    }
    public override void PhysicsUpdate()
    { 

    }
}
