using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Energy Property")]
    [SerializeField] private float _HP;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _shieldHP;
    [SerializeField] bool _invincible = false;

    [Header("Event System")]
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] BossEvent _bossEvent;

    private Rigidbody _rb;
    private AISensor _sensor;
    private float _currentHP;
    private float _currentShield;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sensor = GetComponent<AISensor>();
    }

    void OnEnable()
    {
        _bossEvent.OnBossHurt += BossHurt;
        _gameEvent.OnGameVictory += GameOver;
        _gameEvent.OnGameDefeat += GameOver;
        
        _currentHP = _HP;
        _shieldHP = 0;
    }
    void OnDisable()
    {
        _bossEvent.OnBossHurt -= BossHurt;
        _gameEvent.OnGameVictory -= GameOver;
        _gameEvent.OnGameDefeat -= GameOver;
    }
    
    private void BossHurt(float damage)
    {
        if(_invincible) return;      
        if(_currentShield > 0) return;  
        
        if (_currentHP >= 0)
        {
            _currentHP -= damage;
        }
        if (_currentHP <= 0)
        {
            _currentHP = 0;
            _gameEvent.GameVictory();
        }
    }
    
    private void ShieldHurt(float damage)
    {
        if(_invincible) return;       
        
        if (_shieldHP >= 0)
        {
            _shieldHP -= damage;
        }
        if (_shieldHP <= 0)
        {
            _shieldHP = 0;
        }
    }
    
    public void FaceToPlayer()
    {
        Vector3 dir = _sensor.Target.transform.position - transform.position;
        dir.y = 0;
        _rb.transform.rotation = Quaternion.LookRotation(dir);
    }
    

    private void GameOver()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            _bossEvent.BossHurt(attack.Damage);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            _bossEvent.BossHurt(attack.Damage);
        }
    } 
 
    public void SwinAttack(GameObject bulletPrefab, AttackData attackData)
    {    
        float delayTime = 0;
        foreach(BulletArrayData bulletsArray in attackData._bulletsArray)
        {
            delayTime += bulletsArray._delayTime;
            IEnumerator coroutine = SwinAttackLoop(bulletPrefab, bulletsArray, delayTime);
            StartCoroutine(coroutine);
        }
     
    }
    
    IEnumerator SwinAttackLoop(GameObject bulletPrefab, BulletArrayData bulletsArray, float time)
    {
        yield return new WaitForSeconds(time);
        
        foreach(BulletData bullet in bulletsArray._bullets)
        {
            Vector3 lookDir = Quaternion.Euler(bullet._angle.y, bullet._angle.x, 0) * _rb.transform.forward;
            Quaternion toRotation = Quaternion.LookRotation(lookDir);
            Instantiate(bulletPrefab, _rb.transform.position + new Vector3(0,bullet._height,0), toRotation);
        }     
    }
}
