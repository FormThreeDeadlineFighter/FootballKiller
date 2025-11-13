using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
[System.Serializable]
public class PlayerState_Jump : IPlayerState
{
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _moveSpeed = 5f;
    public override void EnterState()
    { 
        base.EnterState();
        
        _player.Jump(_jumpForce);
        _player.CanJump = false;
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_player.IsFalling && IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(PlayerState_Fall));
        }          
    }
    public override void PhysicsUpdate()
    {
        _player.Move(_moveSpeed);
    }
}
