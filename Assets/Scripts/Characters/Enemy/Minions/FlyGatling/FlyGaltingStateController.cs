using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FlyGaltingStateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;  
    List<IFlyGalting> _shooterStates = new List<IFlyGalting>();
    
    #region States
    #endregion
    
    #region StateConfigs
    #endregion
    void OnEnable()
    {      
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_shooterStates.Count);  
        
        foreach (IFlyGalting state in _shooterStates)
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
