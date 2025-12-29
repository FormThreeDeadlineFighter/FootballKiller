using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Walk", fileName = "PlayerState_Walk")]
[System.Serializable]
public class PlayerState_Walk : IPlayerState
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
