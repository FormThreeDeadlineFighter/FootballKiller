using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Die", fileName = "PlayerState_Die")]
public class PlayerState_Die : IPlayerState
{
    public override void EnterState()
    {
        base.EnterState();
        _player.SetVelocity(Vector3.zero);
    }
    public override void ExitState()
    {
        _player.SetVelocity(Vector3.zero);
        _player.OnPlayerDie();
    }
    public override void LogicUpdate()
    {
        if(IsAnimationComplete)
        {
            _player.OnPlayerDie();
        }
        
    }
    public override void PhysicsUpdate()
    {
        
    }
}
