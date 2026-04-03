using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Minion01StateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;  
    List<IMinion01State> _minion01States = new List<IMinion01State>();
    
    #region States
    Minion01State_Idle _minion01State_Idle;
    Minion01State_Move _minion01State_Move;
    Minion01State_Shoot _minion01State_Shoot;
    #endregion
    
    #region StateConfigs
    [SerializeField] Minion01StateConfig_Idle _idleData;
    [SerializeField] Minion01StateConfig_Move _moveData;
    [SerializeField] Minion01StateConfig_Shoot _shootData;
    #endregion
    void OnEnable()
    {      
        _minion01State_Idle = new Minion01State_Idle(_idleData);
        _minion01State_Move = new Minion01State_Move(_moveData);
        _minion01State_Shoot = new Minion01State_Shoot(_shootData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_minion01States.Count);  
        
        _minion01States.Add(_minion01State_Idle);
        _minion01States.Add(_minion01State_Move);
        _minion01States.Add(_minion01State_Shoot);
        
        foreach (IMinion01State state in _minion01States)
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
