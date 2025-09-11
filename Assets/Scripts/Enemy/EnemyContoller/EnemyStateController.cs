using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : IStateController
{
    [SerializeField] IEnemyState[] _energyStates;
    [SerializeField] GameObject _model;
    private Animator _animator;
    private EnemyController _enemy;
    void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        _animator = _model.GetComponent<Animator>();

        _stateTable = new Dictionary<System.Type, IState>(_energyStates.Length);

        if (_energyStates != null)
        {
            foreach (IEnemyState state in _energyStates)
            {
                CreateState(state);
                state.Initialize(this, _enemy, _animator);
                _stateTable.Add(state.GetType(), state);
            }
        }
        
        SetState(_stateTable[typeof(PlayerState_Idle)]);
    }

    void CreateState(IEnemyState state)
    {
        state = new IEnemyState();
    }
}
