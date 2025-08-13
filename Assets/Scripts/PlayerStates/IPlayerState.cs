using UnityEngine;
using UnityEngine.InputSystem;

public class IPlayerState : ScriptableObject ,IState
{ 
    protected PlayerStateController _stateMachine;
    protected Animator _animator;
    protected PlayerInput _playerInput;
    protected PlayerController _player;

    public void Initialize(PlayerStateController stateMachine, PlayerController player, Animator animator, PlayerInput playerInput)
    {
        _stateMachine = stateMachine;
        _player = player;
        _animator = animator;
        _playerInput = playerInput;
    }
    // when enter state happen
    public virtual void EnterState() { }
    // when exit state happen
    public virtual void ExitState() { }
    // state update, not using physics
    public virtual void LogicUpdate() { }
    // state update, using physics
    public virtual void PhysicsUpdate() { }


}
