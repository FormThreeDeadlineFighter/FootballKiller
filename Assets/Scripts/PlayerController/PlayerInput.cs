using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : MonoBehaviour
{
    PlayerControl _playerControl;
    [SerializeField] float _walkValue;
    [SerializeField] float _runValue;
    Vector2 _stickValue;
    private MoveMode _moveMode = MoveMode.idle;
    [HideInInspector] public MoveMode MoveMode { get { return _moveMode; } private set { MoveMode = _moveMode; } }
    public bool IsJump => _playerControl.Player.Jump.WasPerformedThisFrame();
    
    public void Awake()
    {
        _playerControl = new PlayerControl();  
         
    }

    void OnEnable()
    {   
        _playerControl.Enable();
        _playerControl.Player.Move.performed += OnMovePerformed;
        _playerControl.Player.Move.canceled += OnMovePerformed;
    }

    void OnDisable()
    {
        _playerControl.Player.Move.performed -= OnMovePerformed;
        _playerControl.Player.Move.canceled -= OnMovePerformed;
        _playerControl.Disable();
    }

    void OnMovePerformed(InputAction.CallbackContext value)
    {
       _stickValue = value.ReadValue<Vector2>();

        if (_stickValue == Vector2.zero)
        {
            _moveMode = MoveMode.idle;
        }
        else if(_stickValue.x > _runValue || _stickValue.x < -_runValue || _stickValue.y > _runValue || _stickValue.y < -_runValue)
        {
            _moveMode = MoveMode.run;
        }
        else if (_stickValue.x > _walkValue || _stickValue.x < -_walkValue || _stickValue.y > _walkValue || _stickValue.y < -_walkValue)
        {
            _moveMode = MoveMode.walk;
        }
    }
}

public enum MoveMode {idle, walk, run}
