using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Energy Property")]
    [SerializeField] private float _HP;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _shieldHP;
    [SerializeField] float _attackCD;
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
    
    public bool CanAttack;
    public float PlayerDistance => Vector3.Distance(transform.position, _sensor.Target.transform.position);
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _sensor = GetComponent<AISensor>();
    }

    void OnEnable()
    {
        _gameEvent.OnGameVictory += GameOver;
        _gameEvent.OnGameDefeat += GameOver;
        
        _currentHP = _HP;
        _shieldHP = 0;
    }
    void OnDisable()
    {
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
            _gameEvent.EnemyDestory();
            Destroy(gameObject);
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
    
    public void FaceToPlayer()
    {
        if(_sensor.Target == null) return;
        Vector3 dir = _sensor.Target.transform.position - transform.position;
        dir.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        _rb.transform.rotation = targetRot;
    }
    public void SetVelocity(Vector3 vector3)
    {
        _rb.linearVelocity = vector3;
        _rb.angularVelocity = vector3; 
    }
    
    public void SwitchElement()
    {
        int num = (int)currentElement;
        num = (num + 1)%2;
        currentElement = (Elements)num;
        ElementsShow.material = ElementMaterials[num];
    } 
    
    public bool IsAttackable()
    {
        if(_currentHP > 0) return true;
        else return false;
    }
    void OnDestroy()
    {
        //_gameEvent.EnemyDestory(this.gameObject);
    }
    private void GameOver()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            if(attack.Elements != currentElement) return;
            BossHurt(attack.Damage);
        }
    }
}
