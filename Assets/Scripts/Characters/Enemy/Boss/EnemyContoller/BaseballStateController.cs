using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AISensor))]
public class BaseballStateController : MonoBehaviour
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();

    }
}
