using UnityEngine;
using UnityEngine.InputSystem;

public class IPlayerState : ScriptableObject ,IState
{ 
    protected PlayerStateController _controller;
    protected Animator _animator;
    protected Rigidbody _rb;
    protected PlayerInput _playerInput;
    protected bool IsComplete;

    public void Initialize(PlayerStateController controller, Animator animator, PlayerInput playerInput, Rigidbody rigidbody)
    {
        _controller = controller;
        _animator = animator;
        _playerInput = playerInput;
        _rb = rigidbody;
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
