using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Block", fileName = "PlayerState_Block")]
[System.Serializable]
public class PlayerState_Block : IPlayerState
{
    public override void EnterState()
    { 
        base.EnterState();     
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_player.MoveMode == MoveMode.idle)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }
        if(_player.MoveMode == MoveMode.walk)
        {
            _stateMachine.SetState(typeof(PlayerState_Walk));
        }
        if(_player.MoveMode == MoveMode.run)
        {
            _stateMachine.SetState(typeof(PlayerState_Run));
        }
        if(_input.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
    }
    public override void PhysicsUpdate()
    { 
        
    }
}
