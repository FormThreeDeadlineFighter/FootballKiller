using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : MonoBehaviour
{
    PlayerControl _playerControl;
    [SerializeField] Vector2 _stickValue;
    [SerializeField] float _walkValue;
    [SerializeField] float _runValue;
    
    public MoveMode _playerMoveMode = MoveMode.idle;
    public bool IsJump = false;
    
    public void Awake()
    {
        _playerControl = new PlayerControl();   
    }

    void OnEnable()
    {   
        _playerControl.Enable();
        _playerControl.Player.Move.performed += OnMovePerformed;
        _playerControl.Player.Move.canceled += OnMovePerformed;
        _playerControl.Player.Jump.performed += OnJumpPerformed;
    }

    void OnDisable()
    {
        _playerControl.Disable();
        _playerControl.Player.Move.performed -= OnMovePerformed;
        _playerControl.Player.Move.canceled -= OnMovePerformed;
        _playerControl.Player.Jump.performed -= OnJumpPerformed;
    }

    void OnMovePerformed(InputAction.CallbackContext value)
    {
       _stickValue = value.ReadValue<Vector2>();

        if (_stickValue == Vector2.zero)
        {
            _playerMoveMode = MoveMode.idle;
        }
        else if(_stickValue.x > _runValue || _stickValue.x < -_runValue || _stickValue.y > _runValue || _stickValue.y < -_runValue)
        {
            _playerMoveMode = MoveMode.run;
        }
        else if (_stickValue.x > _walkValue || _stickValue.x < -_walkValue || _stickValue.y > _walkValue || _stickValue.y < -_walkValue)
        {
            _playerMoveMode = MoveMode.walk;
        }
    }
    
    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsJump = true;
        }
    }
}

public enum MoveMode {idle, walk, run}
