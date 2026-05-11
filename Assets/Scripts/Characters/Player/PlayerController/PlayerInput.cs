using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class PlayerInput : MonoBehaviour
{
    private PlayerControl _playerControl;
    public Vector2 StickValue;
    public bool IsJump => _playerControl.Battle.Jump.WasPerformedThisFrame();
    public bool IsBlock => _playerControl.Battle.Block.WasPerformedThisFrame();
    public bool IsDash => _playerControl.Battle.Dash.WasPerformedThisFrame();
    public bool IsLightAttack => _playerControl.Battle.LightAttack.WasPerformedThisFrame();
    public bool IsHeavyAttack => _playerControl.Battle.HeavyAttack.WasPerformedThisFrame();
    public bool IsHold => _playerControl.Battle.HoldAttack.IsPressed();
    public bool IsRelease => _playerControl.Battle.HoldAttack.WasReleasedThisFrame();
    public bool IsSwitch => _playerControl.Battle.SwitchElement.WasPerformedThisFrame();
    public bool IsPause => _playerControl.Battle.Pause.WasPerformedThisFrame();
    public bool IsLock => _playerControl.Battle.Lock.WasPerformedThisFrame();
    public bool IsMove => StickValue != Vector2.zero;
    
    public void Awake()
    {
        _playerControl = new PlayerControl();       
    }

    void OnEnable()
    {   
        _playerControl.Enable();
        _playerControl.Battle.Move.performed += OnMovePerformed;
        _playerControl.Battle.Move.canceled += OnMovePerformed;
    }

    void OnDisable()
    {
        _playerControl.Battle.Move.performed -= OnMovePerformed;
        _playerControl.Battle.Move.canceled -= OnMovePerformed;
        _playerControl.Disable();
    }

    void OnMovePerformed(InputAction.CallbackContext value)
    {
       StickValue = value.ReadValue<Vector2>();      
    }

}
