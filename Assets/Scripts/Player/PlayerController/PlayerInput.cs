using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : MonoBehaviour
{
    PlayerControl _playerControl;
    public Vector2 StickValue;
    public bool IsJump => _playerControl.Player.Jump.WasPerformedThisFrame();
    public bool IsBlock => _playerControl.Player.Block.WasPerformedThisFrame();
    public bool IsShoot => _playerControl.Player.Shoot.WasPerformedThisFrame();
    
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
       StickValue = value.ReadValue<Vector2>();      
    }
}
