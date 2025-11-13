using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Baseball/Idle", fileName = "BaseballState_Idle")]
public class BaseballState_Idle : IEnemyState
{
    // circle attack cooldown
    [SerializeField] float _cooldown;
    // circle attack current cooldown
    float _currentCooldown;
    public override void EnterState()
    {
        base.EnterState();
        _currentCooldown = _cooldown;

    }
    public override void ExitState()
    {
        
    }

    public override void LogicUpdate()
    {
        _currentCooldown -= Time.deltaTime;

        if(_currentCooldown < 0)
        {
            _stateMachine.SetState(typeof(BaseballState_CircleAttack));
        }
    }
    public override void PhysicsUpdate()
    {

    }
}
