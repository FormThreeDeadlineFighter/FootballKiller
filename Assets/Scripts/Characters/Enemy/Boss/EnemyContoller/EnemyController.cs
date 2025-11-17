using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Energy Property")]
    [SerializeField] private float _HP;
    [SerializeField] private float _currentHP;
    // enermy HP 
    public float HP
    {
        get => _currentHP;
        set
        {
            if (value > _HP)
            {
                _currentHP = _HP;
            }
            else if (value < 0)
            {
                _currentHP = 0;
            }
            else
            {
                _currentHP = value;
            }
        }
    }
    // enemy move speed 
    [SerializeField] float _moveSpeed;

    [Header("Event System")]
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] BossEvent _bossEvent;

    private Rigidbody _rb;
    private AISensor _sensor;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sensor = GetComponent<AISensor>();
    }

    void OnEnable()
    {
        _currentHP = _HP;
        _bossEvent.OnBossHurt += BossHurt;
        _gameEvent.OnGameVictory += GameOver;
        _gameEvent.OnGameDefeat += GameOver;
    }
    void OnDisable()
    {
        _bossEvent.OnBossHurt -= BossHurt;
        _gameEvent.OnGameVictory -= GameOver;
        _gameEvent.OnGameDefeat -= GameOver;
    }
    
    public void FaceToPlayer()
    {
        Vector3 dir = _sensor.Target.transform.position - transform.position;
        dir.y = 0;
        _rb.transform.rotation = Quaternion.LookRotation(dir);
    }
    
    private void BossHurt(float damage)
    {
        if (HP >= 0)
        {
            HP -= damage;
        }
        if (HP <= 0)
        {
            _gameEvent.GameVictory();
        }
    }

    private void GameOver()
    {
        Destroy(this.gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            _bossEvent.PlayerHurt(attack.Damage);
        }
    }
}
