using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Energy Property")]
    [SerializeField] private float _HP;
    [SerializeField] float _shieldHP;
    [SerializeField] float _attackCD;
    [SerializeField] float _jumpCD;
    [SerializeField] float rotateSpeed;
    [SerializeField] bool _invincible = false;
    [SerializeField] private Elements currentElement = Elements.white; 
    [SerializeField] Material[] ElementMaterials;  

    [Header("Event System")]
    [SerializeField] Renderer ElementsShow;
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] BossEvent _bossEvent;

    private Rigidbody _rb;
    private AISensor _sensor;
    private float _currentHP;
    private float _currentShield; 
    private bool _TrackPlayer;
    private Coroutine _attackCoroutine;
    private Coroutine _jumpCoroutine;
    
    public Vector3 PlayerPosition => (_sensor.Target.transform.position - _rb.transform.position).normalized;
    public float PlayerDistance => Vector3.Distance(transform.position, _sensor.Target.transform.position);
    public bool CanAttack;
    public bool CanJump;
    public bool IsHurt;
    public bool IsStop;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sensor = GetComponent<AISensor>();
        
        _currentHP = _HP;
        _shieldHP = 0;
        CanAttack = false;
        CanJump = false;
        _TrackPlayer = true;
        IsStop = false;
    }

    void OnEnable()
    {
        _gameEvent.OnGameVictory += EnemyDie;
        _gameEvent.OnGameDefeat += EnemyStop;   
        
        AttackCD();  
        JumpCD();
    }
    void OnDisable()
    {
        _gameEvent.OnGameVictory -= EnemyDie;
        _gameEvent.OnGameDefeat -= EnemyDie;
    }
    void FixedUpdate()
    {
        if(_TrackPlayer)
        {
            FaceToPlayer();
        }
    }

    public void SetVelocity(Vector3 velocity)
    {   
        _rb.linearVelocity = velocity; 
        _rb.angularVelocity = Vector3.zero;     
    }
    public void SetVelocityXZ(Vector3 velocity)
    {
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
        _rb.angularVelocity = Vector3.zero; 
    }
    public void SetVelocityY(float velocityY)
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, velocityY, _rb.linearVelocity.z);
        _rb.angularVelocity = Vector3.zero; 
    }
    
    private void OnHurt(float damage)
    {
        if(_invincible) return;      
        if(_currentShield > 0) return;  
        
        if (_currentHP >= 0)
        {
            _currentHP -= damage;
            IsHurt = true;
            StartCoroutine(Hurt(0.5f));
        }
        if (_currentHP <= 0)
        {
            _currentHP = 0;           
            _bossEvent.BossDie();
        }
        
        float hpPercentage = _currentHP/_HP;
        _bossEvent.BossHPCahange(hpPercentage);
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
    
    public void EnemyStart()
    {
        IsStop = false;
    }
    
    public void EnemyStop()
    {
        IsStop = true;
    }
    
    private void FaceToPlayer()
    {
        if(_sensor.Target == null) return;
        if (_sensor.Target.transform.position == Vector3.zero) return;
        Vector3 dir = _sensor.Target.transform.position - transform.position;
        dir.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        _rb.transform.rotation = Quaternion.Slerp(transform.rotation,targetRot, rotateSpeed * Time.fixedDeltaTime);     
    }
    
    public void IsTrackPlayer()
    {
        _TrackPlayer = true;
    }
    
    public void NotTrackPlayer()
    {
        _TrackPlayer = false;
    }
    
    /*public void SwitchElement()
    {
        int num = (int)currentElement;
        num = (num + 1)%2;
        currentElement = (Elements)num;
        ElementsShow.material = ElementMaterials[num];
    } */
    
    public void AttackCD()
    {
        if (_attackCoroutine != null) return;
        _attackCoroutine = StartCoroutine(AttackCD(_attackCD));
    }
    
    public void JumpCD()
    {
        if (_jumpCoroutine != null) return;
        _jumpCoroutine = StartCoroutine(JumpCD(_attackCD));
    }
    
    public void EnemyDie()
    {
        _gameEvent.EnemyDestory();    
        Destroy(gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            if(attack.Elements != currentElement) return;
            OnHurt(attack.Damage);
        }
    }
    
    IEnumerator AttackCD(float time)
    {
        CanAttack = false;
        yield return new WaitForSeconds(time);
        CanAttack = true;
        StopCoroutine(_attackCoroutine);
        _attackCoroutine = null;
    }
    
    IEnumerator JumpCD(float time)
    {
        CanJump = false;
        yield return new WaitForSeconds(time);
        CanJump = true;
        StopCoroutine(_jumpCoroutine);
        _jumpCoroutine = null;  
    }
    
    IEnumerator Hurt(float time)
    {
        yield return new WaitForSeconds(time);
        IsHurt = false;  
    }
}
