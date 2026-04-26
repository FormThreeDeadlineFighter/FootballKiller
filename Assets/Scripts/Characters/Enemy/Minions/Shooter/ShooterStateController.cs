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
    List<IShooter> _shooterStates = new List<IShooter>();
    
    #region States
    ShooterState_Idle _shooterState_Idle;
    ShooterState_Shoot _shooterState_Shoot;
    ShooterState_Melee _shooterState_Melee;
    ShooterState_PreAttack _shooterState_PreAttack;
    ShooterState_Hurt _shooterState_Hurt;
    ShooterState_Die _shooterState_Die;
    #endregion
    
    #region StateConfigs
    [SerializeField] ShooterStateConfig_Idle _idleData;
    [SerializeField] ShooterStateConfig_Shoot _shootData;
    [SerializeField] ShooterStateConfig_Melee _meleeData;
    [SerializeField] ShooterStateConfig_PreAttack _preAttackData;
    [SerializeField] ShooterStateConfig_Hurt _hurtData;
    [SerializeField] ShooterStateConfig_Die _dieData;
    #endregion
    void OnEnable()
    {      
        _shooterState_Idle = new ShooterState_Idle(_idleData);
        _shooterState_Shoot = new ShooterState_Shoot(_shootData);
        _shooterState_Melee = new ShooterState_Melee(_meleeData);
        _shooterState_PreAttack = new ShooterState_PreAttack(_preAttackData);
        _shooterState_Hurt = new ShooterState_Hurt(_hurtData);
        _shooterState_Die = new ShooterState_Die(_dieData);
        
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
        
        foreach (IShooter state in _shooterStates)
        {
            state.Initialize(this, _enemy, _animator, _director);
            _stateTable.Add(state.GetType(), state);
        }
    }
    void OnDisable()
    {
        _stateTable.Clear();
    }
    
    void Start()
    {
        SetState(_stateTable[typeof(ShooterState_Idle)]);
    }
}
