using UnityEngine;
using UnityEngine.Playables;

public class ITank :IState
{
    float _stateEnterTime;
    protected TankStateController _stateMachine;
    protected Animator _animator;
    protected PlayableDirector _director;
    protected EnemyController _enemy;
    protected bool IsAnimationComplete => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    protected float _stateDuration => Time.time - _stateEnterTime;

    public ITank(IStateConfig stateConfig)
    {
        
    }
    public void Initialize(TankStateController stateMachine, EnemyController enemy, Animator animator, PlayableDirector director)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _animator = animator;
        _director = director;
    }
    // when enter state happen
    public virtual void EnterState() 
    {
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
