using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerStateController : IStateController
{   
    [SerializeField] IPlayerState[] _playerStates;
    [SerializeField] GameObject _model;
    private Animator _animator;
    private PlayerInput _playerInput;
    private PlayerController _player;

    void OnEnable()
    {
        _player = GetComponent<PlayerController>();
        _animator = _model.GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();

        _stateTable = new Dictionary<System.Type, IState>(_playerStates.Length);

        if (_playerStates != null)
        {
            foreach (IPlayerState state in _playerStates)
            {
                state.Initialize(this, _player, _animator, _playerInput);
                _stateTable.Add(state.GetType(), state);
            }
        }

        SetState(_stateTable[typeof(PlayerState_Idle)]);
    }

    void OnDisable()
    {
        _stateTable.Clear();
    }
}
