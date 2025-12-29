using UnityEngine;

public class PlayerState_Turn : IPlayerState
{
    [SerializeField] float _moveSpeed = 5.0f;
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        
    }
    public override void PhysicsUpdate()
    {
        _player.Move(_moveSpeed);
    }
}
