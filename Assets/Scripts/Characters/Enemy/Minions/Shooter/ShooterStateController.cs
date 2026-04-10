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
    List<IShooter> _minion01States = new List<IShooter>();
    
    #region States
    ShooterState_Idle _minion01State_Idle;
    ShooterState_Move _minion01State_Move;
    ShooterState_Shoot _minion01State_Shoot;
    #endregion
    
    #region StateConfigs
    [SerializeField] ShooterStateConfig_Idle _idleData;
    [SerializeField] ShooterStateConfig_Move _moveData;
    [SerializeField] ShooterStateConfig_Shoot _shootData;
    #endregion
    void OnEnable()
    {      
        _minion01State_Idle = new ShooterState_Idle(_idleData);
        _minion01State_Move = new ShooterState_Move(_moveData);
        _minion01State_Shoot = new ShooterState_Shoot(_shootData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_minion01States.Count);  
        
        _minion01States.Add(_minion01State_Idle);
        _minion01States.Add(_minion01State_Move);
        _minion01States.Add(_minion01State_Shoot);
        
        foreach (IShooter state in _minion01States)
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
