using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Shoot", fileName = "PlayerState_Shoot")]
[System.Serializable]
public class PlayerState_Shoot : IPlayerState
{
    [SerializeField] float _duration = 0.1f;
    private float _currentTime;
    public override void EnterState()
    { 
        base.EnterState();    
        _currentTime = _duration;
    }
    public override void ExitState()
    {
        _player.PlayerShoot();
    }
    public override void LogicUpdate()
    {
        _currentTime -= Time.deltaTime;
        
        if (_currentTime <= 0)
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
            if (_input.IsBlock && _player.CanBlock)
            {
                _stateMachine.SetState(typeof(PlayerState_Block));
            }
            if (_input.IsPlayerShoot && _player.CanShoot)
            {
                _stateMachine.SetState(typeof(PlayerState_Shoot));
            }
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
