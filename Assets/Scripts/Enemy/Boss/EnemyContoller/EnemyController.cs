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

    void OnEnable()
    {
        _currentHP = _HP;
    }
    void OnDisable()
    {

    }
    void OnDestroy()
    {

    }
    
    private void EnemyHurt(float damage)
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
}
