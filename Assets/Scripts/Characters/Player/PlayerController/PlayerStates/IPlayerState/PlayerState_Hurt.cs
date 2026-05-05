using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Hurt", fileName = "PlayerState_Hurt")]
public class PlayerState_Hurt : IPlayerState
{
    public override void EnterState()
    {
        base.EnterState();
        _player.SetVelocity(Vector3.zero);
        _player.InvincibleStart(0.5f);
    }
    public override void ExitState()
    {
        _player.IsHurt = false;
    }
    public override void LogicUpdate()
    {
        if(_player.IsDie)
        {
            _stateMachine.SetState(typeof(PlayerState_Die));
        }
        if (IsAnimationComplete)
        {
            if(!_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            }
            if(_player.IsMove)
            {
                _stateMachine.SetState(typeof(PlayerState_Move));
            }           
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
