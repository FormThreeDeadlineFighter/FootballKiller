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
    [SerializeField, Range(0,1)] float _walkValue;
    [SerializeField, Range(0, 1)] float _runValue;
    [SerializeField] float _dashForce = 50f;
    

    [Header("Player Objects")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] GameObject _playerHitBox;
    [SerializeField] GameObject _attackHitBox;
    [SerializeField] Material[] ElementMaterials;
    [SerializeField] Renderer ElementsShow;
    [SerializeField] PlayerEvent _playerEvents;
    [SerializeField] GameEvent _gameEvent;
    
    private Rigidbody _rb;
    private PlayerGroundDetector _groundDetector;
    private PlayerInput _input;
    private EnergyController _energyController;
    private PlayerMeleeCombat _combot;
    private bool Invincible = false;
    private ComboGrade _currentGrade;
    
    public bool IsGrounded => _groundDetector.IsGrounded;
    public bool IsFalling => _rb.linearVelocity.y < 0 && !IsGrounded;
    public bool IsMove => _input.IsMove;
    public bool CanJump = false;
    public bool AttackCancel = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        _energyController = GetComponentInChildren<EnergyController>();
        _combot = GetComponent<PlayerMeleeCombat>();      
    }

    void OnEnable()
    {   
        _playerEvents.OnPlayerHurt += GetHurt;
        _playerEvents.OnComboGradeChange += GetComboGrade;
        
        _playerHitBox.SetActive(true); 
        _currentHP = _HP;
        CanJump = true;
    }

    void OnDisable()
    {
        _playerEvents.OnPlayerHurt -= GetHurt;
        _playerEvents.OnComboGradeChange -= GetComboGrade;
        _playerHitBox.SetActive(false);
    }
    void Update()
    {
        if(_input.IsSwitch)
        {
            IAttack attack = _attackHitBox.GetComponent<IAttack>();
            int num = (int)attack.Elements;
            num = (num + 1)%2;
            attack.Elements = (Elements)num;
            ElementsShow.material = ElementMaterials[num];
        }
    }

    public void SetVelocity(Vector3 velocity)
    {   
        _rb.linearVelocity = velocity;      
    }
    public void SetVelocityXZ(Vector3 velocity)
    {
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }

    public void SetVelocityY(float velocityY)
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, velocityY, _rb.linearVelocity.z);
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
            moveDir = moveDir.normalized;
        }
        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            _rb.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
            SetVelocityXZ(moveDir * speed);
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
            SetVelocityXZ(moveDir * _dashForce);
        }
        else
        {
            SetVelocityXZ(_rb.transform.forward * _dashForce);
        }      
    }

    public void Jump(float speed)
    {
        SetVelocityY(speed);
        SetVelocityXZ(_rb.linearVelocity);
    }
    
    public void AttackDataInput(float damage, float comboChange)
    {
        _combot.SelectTarget();
        _combot.MoveTowardsTarget();
              
        switch (_currentGrade)
        {
            case ComboGrade.C: damage *= 1.1f;
            break;
            case ComboGrade.B: damage *= 1.3f;
            break;
            case ComboGrade.A: damage *= 1.5f;
            break;
            case ComboGrade.S: damage *= 2f;
            break;
            default: 
            break;
        }
        _attackHitBox.GetComponent<IAttack>().Damage = damage;
        _playerEvents.PlayerComboChange(comboChange);
    }
    public void AttackEnter()
    {
        _attackHitBox.SetActive(true);
    }
    
    public void AttackExit()
    {
        _attackHitBox.SetActive(false);
        AttackCancel = true;
    }
    
    public void BlockEnter()
    {
        
    }
    
    public void BlockExit()
    {
        
    }
    private void GetComboGrade(ComboGrade grade)
    {
        _currentGrade = grade;
    }

    public void GetInvincible(float time)
    {
        IEnumerator coroutine = InvincibleTime(time);
        StartCoroutine(coroutine);
    }
    
    private void GetHurt(float damage)
    {
        if (Invincible) return;
        if (_currentHP >= 0)
        {
            _currentHP -= damage;
        }
        if (_currentHP <= 0)
        {
            _gameEvent.GameDefeat();
        }
        
        float hpPercentage = _currentHP/_HP;
        _playerEvents.PlayerHPChange(hpPercentage);
    }

    IEnumerator InvincibleTime(float time)
    {
        Invincible = true;
        yield return new WaitForSeconds(time);
        Invincible = false;
    } 
}
