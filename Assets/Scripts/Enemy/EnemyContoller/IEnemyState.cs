using UnityEngine;

public class IEnemyState : ScriptableObject ,IState
{
    [SerializeField] string _animationName;
    [SerializeField, Range(0f, 1f)] float _transitionDuration = 0.1f;
    float _stateEnterTime;
    int _stateHash; 
    protected EnemyStateController _stateMachine;
    protected Animator _animator;
    protected EnemyController _enemy;
    protected bool IsAnimationComplete => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    protected float _stateDuration => Time.time - _stateEnterTime;
    
    void OnEnable()
    {
        _stateHash = Animator.StringToHash(_animationName);
    }
    public void Initialize(EnemyStateController stateMachine, EnemyController enemy, Animator animator)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _animator = animator;
    }
    // when enter state happen
    public virtual void EnterState() 
    {
        Debug.Log($"enemy enter {this}");
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
