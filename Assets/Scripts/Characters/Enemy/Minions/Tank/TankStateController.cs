using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TankStateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;  
    List<ITank> _tankStates = new List<ITank>();
    
    #region States

    #endregion
    
    #region StateConfigs

    #endregion
    void OnEnable()
    {      
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_tankStates.Count);  
    
        foreach (ITank state in _tankStates)
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
