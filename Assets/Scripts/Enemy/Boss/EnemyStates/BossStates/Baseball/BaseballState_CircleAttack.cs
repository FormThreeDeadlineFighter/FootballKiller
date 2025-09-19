using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Baseball/CircleAttack", fileName = "BaseballState_CircleAttack")]
public class BaseballState_CircleAttack : IEnemyState
{
    [SerializeField] GameObject _attack;
    // each bullet between angle
    [SerializeField, Range(0,360)] float _spreadAngle = 60;
    [SerializeField] float _spawnPosition = 10;
    [SerializeField] float _cooldown = 1;
    float _currentTime;

    public override void EnterState()
    {
        base.EnterState();
        _currentTime = _cooldown;
    }
    public override void ExitState()
    {
        if (_attack != null)
        {
            int num = Random.Range(1, 3);
            _attack.GetComponent<IAttack>().Elements = (Elements)num;
            
        }
    }

    public override void LogicUpdate()
    {
        ShootCircleAttack();
        _cooldown -= Time.deltaTime;
        ShootCircleAttack();

        if (IsAnimationComplete)
        {
            _stateMachine.SetState(typeof(BaseballState_Idle));
        }
    }
    public override void PhysicsUpdate()
    {
        
    }

    void ShootCircleAttack()
    {
        // attack spawn position
        float positionY = _enemy.transform.position.y - _spawnPosition;
        Vector3 position = new Vector3(0, positionY, 0) + _enemy.transform.position;
        // change attack rotate
        float angle = _enemy.transform.eulerAngles.y;
        for (int i = 0; i < 360 / _spreadAngle; i++)
        {
            Instantiate(_attack, position, Quaternion.Euler(0, angle, 0));
            angle += _spreadAngle;
        }
    }
}
