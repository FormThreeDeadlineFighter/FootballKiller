using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AISensor))]
public class BaseballStateController : MonoBehaviour
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    private float _timer = 5;
    private float _currentTime;
    void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        
        _currentTime = _timer;
    }

    private void FixedUpdate() 
    {
        if(_currentTime < 0)
        {
            _animator.SetTrigger("Swin");
            _currentTime = _timer;
        }
        _currentTime -= Time.fixedDeltaTime;
    }

}
