using UnityEngine;
using UnityEngine.InputSystem;

public class EasyAnimator : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpHeight = 2f;
    Animator _animator;
    Rigidbody _rb;
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _sprintAction;
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        onJump();
        OnMove();
        //onSprint();
    }
    void OnMove()
    {
        if (_moveAction.IsPressed())
        {
            _animator.SetBool("isWalking", true);
            Vector2 _moveAmount = _moveAction.ReadValue<Vector2>();
            _animator.SetFloat("Velocity X", _moveAmount.x);
            _animator.SetFloat("Velocity Y", _moveAmount.y);
            transform.position += new Vector3(_moveAmount.x, 0f, _moveAmount.y) * speed * Time.deltaTime;
            Debug.Log(_moveAmount);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

    }
    void onJump()
    {
        if (_jumpAction.IsPressed())
        {
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime);
            _animator.SetBool("isJump", true);
        }
        else
        {
            _animator.SetBool("isJump", false);
        }
    }
    void onSprint()
    {
        if (_sprintAction.IsPressed())
        {
            _animator.SetBool("isSprint", true);
        }
        else
        {
            _animator.SetBool("isSprint", false);
        }
    }
}
