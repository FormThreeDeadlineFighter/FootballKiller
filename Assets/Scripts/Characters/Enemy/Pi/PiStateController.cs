using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody), typeof(AISensor))]
public class PiStateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;
    List<IPiState> _piStates = new List<IPiState>();
    [SerializeField] StateConfig_Pi _data;
    
    #region States
    #endregion

    void OnEnable()
    {
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_piStates.Count);
        
        
        foreach (IPiState state in _piStates)
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
        //SetState(_stateTable[typeof(BaseballState_Idle)]);
    }
    
}
