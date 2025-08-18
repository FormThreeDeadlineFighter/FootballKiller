using System;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerGroundDetector _groundDetector;
    private PlayerInput _input;
    private BlockController _blockController;
    public bool IsGrounded => _groundDetector.IsGrounded;
    public bool IsFall => !IsGrounded;
    public bool CanJump = false;
    public bool IsBlock => _blockController.IsBlock;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        _groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        _blockController = GetComponentInChildren<BlockController>(); 
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
