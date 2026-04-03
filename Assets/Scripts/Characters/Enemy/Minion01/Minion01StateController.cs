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
    List<IMinion01> _minion01States = new List<IMinion01>();
    
    #region States
    Minion01State_Idle _minion01State_Idle;
    #endregion
    
    #region StateConfigs
    [SerializeField] Minion01StateConfig_Idle _idleData;
    #endregion
    void OnEnable()
    {      
        _minion01State_Idle = new Minion01State_Idle(_idleData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_minion01States.Count);  
        
        _minion01States.Add(_minion01State_Idle);
        
        foreach (IMinion01 state in _minion01States)
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
