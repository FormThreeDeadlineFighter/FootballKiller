using System.Collections.Generic;
using UnityEngine;

public class BaseballStateController : IStateController
{
    [SerializeField] IEnemyState[] _energyStates;
    // Enemy model
    [SerializeField] GameObject _model;
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        _animator = _model.GetComponent<Animator>();

        // creat a state dictionary 
        _stateTable = new Dictionary<System.Type, IState>(_energyStates.Length);
        
        // put baseball boss state into stateTable
        if (_energyStates != null)
        {
            foreach (IEnemyState state in _energyStates)
            {
                state.Initialize(this, _enemy, _animator);
                _stateTable.Add(state.GetType(), state);
            }
        }

        SetState(_stateTable[typeof(BaseballState_Idle)]);
    }
}
