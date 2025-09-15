using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Baseball/CircleAttack", fileName = "BaseballState_CircleAttack")]
public class BaseballState_CircleAttack : IEnemyState
{
    [SerializeField] GameObject _attack;
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void ExitState()
    {
        if (_attack != null)
        {
            Instantiate(_attack, _enemy.transform.position, new Quaternion(0, 0, _enemy.transform.rotation.z - 30,0));
            Instantiate(_attack, _enemy.transform.position, new Quaternion(0, 0, _enemy.transform.rotation.z,0));
            Instantiate(_attack, _enemy.transform.position, new Quaternion(0, 0, _enemy.transform.rotation.z + 30,0));
        }
    }

    public override void LogicUpdate()
    {
        if (IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(BaseballState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
