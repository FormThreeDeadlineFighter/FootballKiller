using Unity.Mathematics.Geometry;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
[System.Serializable]
public class PlayerState_Run : IPlayerState
{
    [SerializeField] float _moveSpeed = 7.0f;
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
        if(_input.IsJump && _player.CanJump && _player.IsGrounded)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        if (_input.IsBlock && _player.CanBlock)
        {
            _stateMachine.SetState(typeof(PlayerState_Block));
        }
        if (_input.IsPlayerShoot && _player.CanShoot)
        {
            _stateMachine.SetState(typeof(PlayerState_Shoot));
        }
        if(_input.IsDash)
        {
            _stateMachine.SetState(typeof(PlayerState_Dash));
        }
        if(_input.IsRetrieve &&  _player.CanRetrieve)
        {
            _stateMachine.SetState(typeof(PlayerState_Retrieve));
        }
    }
    public override void PhysicsUpdate()
    { 
        _player.Move(_moveSpeed);
    }
}
