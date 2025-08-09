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
        _playerControl.Disable();
        _playerControl.Player.Move.performed -= OnMovePerformed;
        _playerControl.Player.Move.canceled -= OnMovePerformed;
    }
    void Update()
    {
        if(_stickValue.x > _runValue || _stickValue.x < -_runValue || _stickValue.y > _runValue || _stickValue.y < -_runValue)
        {
            Debug.Log("player run");
        }
        else if (_stickValue.x > _walkValue || _stickValue.x < -_walkValue || _stickValue.y > _walkValue || _stickValue.y < -_walkValue)
        {
            Debug.Log("player walk");
        }
    }

    void OnMovePerformed(InputAction.CallbackContext value)
    {
       _stickValue = value.ReadValue<Vector2>();
    }
}
