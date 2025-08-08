using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerStateController : IStateController
{   
    [SerializeField] IPlayerState[] _playerStates;
    private Animator _animator;
    private Rigidbody _rb;
    private PlayerControl _playerControl; 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerControl = GetComponent<PlayerControl>();
        _rb = GetComponent<Rigidbody>();
        
        _stateTable = new Dictionary<System.Type, IState>(_playerStates.Length);
      
        foreach(IPlayerState state in _playerStates)
        {  
            state.Initialize(this, _animator, _playerControl, _rb);
            _stateTable.Add(state.GetType(), state);
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
        //SetState(_stateTable[typeof(PlayerState_Idle)]);
    }
}
