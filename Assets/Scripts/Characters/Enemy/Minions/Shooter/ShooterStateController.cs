using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShooterStateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;  
    List<IShooterState> _shooterStates = new List<IShooterState>();
    [SerializeField] StateConfig_Shooter _data;
    
    
    #region States
    ShooterState_Idle _shooterState_Idle;
    ShooterState_Shoot _shooterState_Shoot;
    ShooterState_Melee _shooterState_Melee;
    ShooterState_PreAttack _shooterState_PreAttack;
    ShooterState_Hurt _shooterState_Hurt;
    ShooterState_Die _shooterState_Die;
    #endregion

    void OnEnable()
    {      
        _shooterState_Idle = new ShooterState_Idle();
        _shooterState_Shoot = new ShooterState_Shoot();
        _shooterState_Melee = new ShooterState_Melee();
        _shooterState_PreAttack = new ShooterState_PreAttack();
        _shooterState_Hurt = new ShooterState_Hurt();
        _shooterState_Die = new ShooterState_Die();
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_shooterStates.Count);  
        
        _shooterStates.Add(_shooterState_Idle);
        _shooterStates.Add(_shooterState_Shoot);
        _shooterStates.Add(_shooterState_Melee);
        _shooterStates.Add(_shooterState_PreAttack);
        _shooterStates.Add(_shooterState_Hurt);
        _shooterStates.Add(_shooterState_Die);
        
        foreach (IShooterState state in _shooterStates)
        {
            state.Initialize(this, _enemy, _animator, _director, _data);
            _stateTable.Add(state.GetType(), state);
        }
    }
    void OnDisable()
    {
        _stateTable.Clear();
    }
    
    void Start()
    {
        SetState(_stateTable[typeof(PiState_Idle)]);
    }
}
