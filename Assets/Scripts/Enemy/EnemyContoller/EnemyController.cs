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

    [Header("Energy Objects")]
    [SerializeField] GameEvent _gameEvent;

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
}
