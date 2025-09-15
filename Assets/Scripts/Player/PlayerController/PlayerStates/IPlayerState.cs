using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class IPlayerState : ScriptableObject ,IState
{
    [SerializeField] string _animationName;
    [SerializeField, Range(0f, 1f)] float _transitionDuration = 0.1f;
    float _stateEnterTime;
    int _stateHash; 
    protected PlayerStateController _stateMachine;
    protected Animator _animator;
    protected PlayerInput _input;
    protected PlayerController _player;
    protected bool IsAnimationComplete => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    protected float _stateDuration => Time.time - _stateEnterTime;
    
    void OnEnable()
    {
        _stateHash = Animator.StringToHash(_animationName);
    }
    public void Initialize(PlayerStateController stateMachine, PlayerController player, Animator animator, PlayerInput playerInput)
    {
        _stateMachine = stateMachine;
        _player = player;
        _animator = animator;
        _input = playerInput;
    }
    // when enter state happen
    public virtual void EnterState() 
    {
        Debug.Log($"player enter {this}");
        _animator.CrossFade(_stateHash, _transitionDuration);
        _stateEnterTime = Time.time;
    }
    // when exit state happen
    public virtual void ExitState() 
    { 
    
    }
    // state update, not using physics
    public virtual void LogicUpdate() 
    { 
    
    }
    // state update, using physics
    public virtual void PhysicsUpdate() 
    { 
    
    }


}
