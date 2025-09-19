using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Block", fileName = "PlayerState_Block")]
[System.Serializable]
public class PlayerState_Block : IPlayerState
{
    [SerializeField] float _moveSpeed = 2.0f;
    [SerializeField] float _duration = 0.1f;
    private float _currentTime;
    public override void EnterState()
    {
        base.EnterState();
        _player.BlockEnter();
        _currentTime = _duration;
    }
    public override void ExitState()
    {
        _player.BlockExit();
    }
    public override void LogicUpdate()
    {
        if (!_input.IsBlock && _currentTime <= 0)
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
            if (_input.IsBlock && _player.CanBlock)
            {
                _stateMachine.SetState(typeof(PlayerState_Block));
            }
            if (_input.IsShoot && _player.CanShoot)
            {
                _stateMachine.SetState(typeof(PlayerState_Shoot));
            }
        }      
        
        _currentTime -= Time.deltaTime;
    }
    public override void PhysicsUpdate()
    {
        _player.Move(_moveSpeed);
    }
}
