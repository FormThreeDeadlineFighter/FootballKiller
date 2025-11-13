using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/EnemyState/Baseball/CircleAttack", fileName = "BaseballState_CircleAttack")]
public class BaseballState_CircleAttack : IEnemyState
{
    [SerializeField] GameObject _attack;
    // each bullet between angle
    [SerializeField, Range(0,360)] float _spreadAngle = 60;
    [SerializeField] float _spawnPosition = 10;
    [SerializeField] float _duration = 0.5f;
    [SerializeField] int _loopTime = 2;
    private float _currentTime;
    private int _currentLoopTime;

    public override void EnterState()
    {
        base.EnterState();
        _currentTime = _duration;
    }
    public override void ExitState()
    {
        if (_attack != null)
        {
            // random color attack
            int num = Random.Range(1, 3);
            _attack.GetComponent<IAttack>().Elements = (Elements)num;

            // attack spawn position
            _enemy.FaceToPlayer();
            float positionY = _enemy.transform.position.y - _spawnPosition;
            Vector3 position = new Vector3(0, positionY, 0) + _enemy.transform.position;

            // change attack rotate
            float angle = _enemy.transform.eulerAngles.y;
            for (int i = 0; i < 360 / _spreadAngle; i++)
            {
                Instantiate(_attack, position, Quaternion.Euler(0, angle, 0));
                angle += _spreadAngle;
            }

            _currentLoopTime -= 1;
        }
    }

    public override void LogicUpdate()
    {
        if (IsAnimationComplete && _currentTime <= 0)
        {
            if (_currentLoopTime > 0)
            {
                _stateMachine.SetState(typeof(BaseballState_CircleAttack));
            }
            else
            {
                _currentLoopTime = _loopTime;
                _stateMachine.SetState(typeof(BaseballState_Idle));
            }
        }

        _currentTime -= Time.deltaTime;
    }
    public override void PhysicsUpdate()
    {
        
    }
}
