using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [Header("Player Property")]
    [SerializeField] float _HP = 100f;
    [SerializeField] float _currentHP;
    public float HP 
    {
        get => _currentHP;
        set 
        { 
            if(value > _HP)
            {
                _currentHP = _HP;
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
    [SerializeField, Range(0, 1)] float _runValue;
    [SerializeField] float _dashForce = 50f;
    

    [Header("Player Objects")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _ballTransform;
    [SerializeField] GameObject _blockDetector;
    [SerializeField] GameObject _bodyDetector;
    [SerializeField] PlayerEvent _playerEvents;
    [SerializeField] GameEvent _gameEvent;

    private Rigidbody _rb;
    private PlayerGroundDetector _groundDetector;
    private PlayerInput _input;
    private EnergyController _energyController;
    
    public bool IsGrounded => _groundDetector.IsGrounded;
    public bool IsFalling => _rb.linearVelocity.y < 0 && !IsGrounded;
    public bool CanBlock => _energyController.CanBlock;
    public bool CanShoot => _energyController.CanShoot;
    public bool CanJump = false;
    private bool Invincible = false;
    
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

        HP = _HP;
    }

    void OnEnable()
    {   
        CanJump = true;
        _playerEvents.OnPlayerHurt += GetHurt;
        _bodyDetector.SetActive(true);
    }

    void OnDisable()
    {
        _playerEvents.OnPlayerHurt -= GetHurt;
        _bodyDetector.SetActive(false);
    }
    
    void Update()
    {
        if (_input.IsRobotShoot)
        {
            RobotShoot();
        } 
    }
    
    public void SetVelocity(Vector3 velocity)
    {
        if(velocity != null)
        {
            _rb.linearVelocity = velocity;
        }
    }
    public void SetVelocityX(float velocityX)
    {
        _rb.linearVelocity = new Vector3(velocityX, _rb.linearVelocity.y, _rb.linearVelocity.z);
    }

    public void SetVelocityY(float velocityY)
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, velocityY, _rb.linearVelocity.z);
    }
    public void SetVelocityZ(float velocityZ)
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, velocityZ);
    }

    public void Move(float speed)
    {
        Vector3 moveDir = _rb.transform.forward * _input.StickValue.y + _rb.transform.right * _input.StickValue.x;
        if (_cameraTransform != null)
        {
            Vector3 camForward = _cameraTransform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            moveDir = camForward * _input.StickValue.y + camRight * _input.StickValue.x;
        }
        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            _rb.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
            SetVelocityX(moveDir.x * speed);
            SetVelocityZ(moveDir.z * speed);
        }
        
    }
    
    public void Dash()
    {
        Vector3 moveDir = _rb.transform.forward * _input.StickValue.y + _rb.transform.right * _input.StickValue.x;
        if (_cameraTransform != null)
        {
            Vector3 camForward = _cameraTransform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            moveDir = camForward * _input.StickValue.y + camRight * _input.StickValue.x;
        }
        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            _rb.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
            SetVelocityX(moveDir.x * _dashForce);
            SetVelocityZ(moveDir.z * _dashForce);
        }
        else
        {
            SetVelocity(_rb.transform.forward * _dashForce);
        }
        Debug.Log("dash");      
    }

    public void Jump(float speed)
    {
        SetVelocityY(speed);
        SetVelocityX(_rb.linearVelocity.x);
        SetVelocityZ(_rb.linearVelocity.z);
    }
    
    public void PlayerShoot()
    {
        _energyController.OnPlayerShoot();
    }
    
    public void RobotShoot()
    {       
        _energyController.OnRobotShoot();
    }

    public void BlockEnter()
    {
        _blockDetector.SetActive(true);
        _energyController.EnergyUse(10f);
    }
    public void BlockExit()
    {
        _blockDetector.SetActive(false);
    }

    public void GainEnergy(float value)
    {
        _energyController.EnergyGain(value);
    }

    public void StartInvincible(float time)
    {
        IEnumerator coroutine = InvincibleTime(time);
        StartCoroutine(coroutine);
    }

    private void GetHurt(float damage)
    {
        if (Invincible) return;
        if (HP >= 0)
        {
            HP -= damage;
        }
        if (HP <= 0)
        {
            _gameEvent.GameDefeat();
        }
    }

    IEnumerator InvincibleTime(float time)
    {
        Invincible = true;
        yield return new WaitForSeconds(time);
        Invincible = false;
    }
}
public enum MoveMode {idle, walk, run}
