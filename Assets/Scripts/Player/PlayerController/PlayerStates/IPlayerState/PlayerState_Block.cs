using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Block", fileName = "PlayerState_Block")]
[System.Serializable]
public class PlayerState_Block : IPlayerState
{
    [SerializeField] float _moveSpeed = 2.0f;
    [SerializeField] float _duration = 0.1f;
    private float _blockTime;
    public override void EnterState()
    {
        base.EnterState();
        _player.PlayerBlockEnter();
        _blockTime = _duration;
    }
    public override void ExitState()
    {
        _player.PlayerBlockExit();
    }
    public override void LogicUpdate()
    {
        _blockTime -= Time.deltaTime;

        if (!_input.IsBlock && _blockTime <= 0)
        {
            if (_player.MoveMode == MoveMode.idle)
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            }
            if (_player.MoveMode == MoveMode.walk)
            {
                _stateMachine.SetState(typeof(PlayerState_Walk));
            }
            if (_player.MoveMode == MoveMode.run)
            {
                _stateMachine.SetState(typeof(PlayerState_Run));
            }
            if (_input.IsJump && _player.CanJump && _player.IsGrounded)
            {
                _stateMachine.SetState(typeof(PlayerState_Jump));
            }
        }           
    }
    public override void PhysicsUpdate()
    {
        _player.PlayerMove(_moveSpeed);
    }
}
