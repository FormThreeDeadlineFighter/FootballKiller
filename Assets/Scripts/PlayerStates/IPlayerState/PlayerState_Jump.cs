using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
[System.Serializable]
public class PlayerState_Jump : IPlayerState
{
    [SerializeField] float _jumpForce = 5f;
    public override void EnterState()
    { 
        base.EnterState();
        
        _player.SetForceY(_jumpForce);
        _player.CanJump = false;
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(_player.IsFall && IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(PlayerState_Falling));
        }          
    }
    public override void PhysicsUpdate()
    {
         
    }
}
