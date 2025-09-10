using System;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [Header("Player Property")]
    [SerializeField] float _maxHP = 100;
    [SerializeField] float _currentHP;
    public float HP 
    {
        get => _currentHP;
        set 
        { 
            if(value > _maxHP)
            {
                _currentHP = _maxHP;
            }
            else if(value < 0)
            {
                _currentHP = 0;
            }
            else
            {
                _currentHP = value; 
            }
        }
    }
    [SerializeField, Range(0,1)] float _walkValue;
    [SerializeField, Range(0,1)] float _runValue;

    [Header("Player Objects")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] GameObject _blockDetector;
    [SerializeField] PlayerEvents _playerEvents;
    [SerializeField] GameEvent _gameEvent;

    private Rigidbody _rb;
    private PlayerGroundDetector _groundDetector;
    private PlayerInput _input;
    private EnergyController _energyController;
    
    public bool IsGrounded => _groundDetector.IsGrounded;
    public bool IsFalling => _rb.linearVelocity.y < 0 && !IsGrounded;
    public bool CanJump = false;
    public MoveMode MoveMode 
    { 
        get {
                if (_input.StickValue.sqrMagnitude >= _runValue * _runValue)
                {
                    return MoveMode.run;
                }
                else if(_input.StickValue.sqrMagnitude >= _walkValue * _walkValue)
                {
                    return MoveMode.walk;
                }
                return MoveMode.idle; 
        } 
    }


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        _energyController = GetComponentInChildren<EnergyController>();

        HP = _maxHP;
    }

    void OnEnable()
    {   
        CanJump = true;
        _playerEvents.OnPlayerHurt += PlayerHurt;
    }

    void OnDisable()
    {
        _playerEvents.OnPlayerHurt -= PlayerHurt;
    }
    public void SetVelocityY(float speed)
    {
        _rb.linearVelocity += Vector3.up * speed;
    }
    public void PlayerMove(float speed)
    {
        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight   = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * _input.StickValue.y + camRight * _input.StickValue.x;
        
        //if(Vector3.Angle(camForward, moveDir) > 45)
        
        if(moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            _rb.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
            _rb.transform.position += moveDir *  speed * Time.fixedDeltaTime;
        }
    }
    
    public void PlayerShoot()
    {
        _energyController.OnShoot();
    }

    public void PlayerBlockEnter()
    {
        _blockDetector.SetActive(true);
    }
    public void PlayerBlockExit()
    {
        _blockDetector.SetActive(false);
    }

    private void PlayerHurt(float damage)
    {
        if (HP >= 0)
        {
            HP -= damage;
        }
        if (HP <= 0)
        {
            _gameEvent.GameDefeat();
        }
    }
    
    
}
public enum MoveMode {idle, walk, run}
