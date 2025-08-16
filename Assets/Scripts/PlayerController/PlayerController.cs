using System;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerGroundDetector _groundDetector;
    private PlayerInput _input;
    public bool IsGrounded => _groundDetector.IsGrounded;
    public bool IsFall => !IsGrounded;
    public bool CanJump = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _groundDetector = GetComponentInChildren<PlayerGroundDetector>();       
    }

    void OnEnable()
    {   
        CanJump = true;
    }

    void OnDisable()
    {
    
    }

    public void SetForceY(float force)
    {
        _rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
    
    
}
