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
    ShooterState_Move _shooterState_Move;
    ShooterState_Shoot _shooterState_Shoot;
    #endregion
    
    #region StateConfigs
    [SerializeField] ShooterStateConfig_Idle _idleData;
    [SerializeField] ShooterStateConfig_Move _moveData;
    [SerializeField] ShooterStateConfig_Shoot _shootData;
    #endregion
    void OnEnable()
    {      
        _shooterState_Idle = new ShooterState_Idle(_idleData);
        _shooterState_Move = new ShooterState_Move(_moveData);
        _shooterState_Shoot = new ShooterState_Shoot(_shootData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_shooterStates.Count);  
        
        _shooterStates.Add(_shooterState_Idle);
        _shooterStates.Add(_shooterState_Move);
        _shooterStates.Add(_shooterState_Shoot);
        
        foreach (IShooter state in _shooterStates)
        {
            state.Initialize(this, _enemy, _animator, _director);
            _stateTable.Add(state.GetType(), state);
        }
    }
    
    void OnDisable()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void OnDie()
    {
        
    }
}
