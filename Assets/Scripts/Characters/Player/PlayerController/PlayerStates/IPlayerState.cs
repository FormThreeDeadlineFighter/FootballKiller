using UnityEngine;
using UnityEngine.Playables;
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
    protected PlayableDirector _director;
    protected PlayerInput _input;
    protected PlayerController _player;
    protected bool IsAnimationComplete => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    protected float _stateDuration => Time.time - _stateEnterTime;
    
    void OnEnable()
    {
        _stateHash = Animator.StringToHash(_animationName);
    }
    public void Initialize(PlayerStateController stateMachine, PlayerController player, Animator animator, PlayableDirector director, PlayerInput playerInput)
    {
        _stateMachine = stateMachine;
        _player = player;
        _animator = animator;
        _director = director;
        _input = playerInput;
    }
    // when enter state happen
    public virtual void EnterState() 
    {
        _animator.CrossFade(_stateHash, _transitionDuration);
        _stateEnterTime = Time.time;
        Debug.Log(this.name);
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
