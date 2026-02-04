using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
[System.Serializable]
public class PlayerState_Idle : IPlayerState
{
    [SerializeField] AnimationCurve _speedCurve;
    public override void EnterState()
    {
        base.EnterState();
        _player.SetVelocity(Vector3.zero);
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_player.IsMove)
        {
            _stateMachine.SetState(typeof(PlayerState_Run));
        }
        if(_input.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        if(_player.IsFalling)
        {
            _stateMachine.SetState(typeof(PlayerState_Fall));
        }       
        if(_input.IsBlock)
        {
            _stateMachine.SetState(typeof(PlayerState_Block));
        }
        if(_input.IsDash)
        {
            _stateMachine.SetState(typeof(PlayerState_Dash));
        }
        if(_input.IsLightAttack)
        {
            _stateMachine.SetState(typeof(PlayerState_LightAttack1));
        } 
    }
    public override void PhysicsUpdate()
    {
        
    }
}
