using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Dash", fileName = "PlayerState_Dash")]
public class PlayerState_Dash : IPlayerState
{
    [SerializeField] float _duration = 0.1f;
    private float _currentTime;
    public override void EnterState()
    {
        base.EnterState();

        _player.Dash();
        _currentTime = _duration;
    }
    public override void ExitState()
    {
        _player.SetVelocity(Vector3.zero);
    }
    public override void LogicUpdate()
    {
        if (_currentTime <= 0)
        {
            if(!_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            }
            if(_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Run));
            }           
        }
        _currentTime -= 1;
    }
    public override void PhysicsUpdate()
    {
        
    }
}
