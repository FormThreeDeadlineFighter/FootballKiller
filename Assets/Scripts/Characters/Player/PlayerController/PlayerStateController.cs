using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
[RequireComponent(typeof(PlayerControl))]
public class PlayerStateController : IStateController
{   
    //[SerializeField] GameObject _modelHead;
    [SerializeField] GameObject _modelFoot;
    [SerializeField] IPlayerState[] _playerStates;
    [SerializeField] GameEvent _gameEvent;
    private Animator _animator;
    private PlayableDirector _director;
    private PlayerInput _playerInput;
    private PlayerController _player; 

    void Awake()
    {
        _player = GetComponent<PlayerController>();
        _animator = _modelFoot.GetComponent<Animator>();
        _director = GetComponent<PlayableDirector>();
        _playerInput = GetComponent<PlayerInput>();

        _stateTable = new Dictionary<System.Type, IState>(_playerStates.Length);

        if (_playerStates != null)
        {
            foreach (IPlayerState state in _playerStates)
            {
                state.Initialize(this, _player, _animator, _director,_playerInput);
                _stateTable.Add(state.GetType(), state);
            }
        }    
    }
    void OnEnable()
    {
        _gameEvent.OnGameDefeat += OnDie;
    }
    
    void OnDisable()
    {
        _gameEvent.OnGameDefeat -= OnDie;
        
        _stateTable.Clear();
    }
    void Start()
    {
        SetState(_stateTable[typeof(PlayerState_Idle)]);
    }
    
    void OnDie()
    {
        
    }
    
}
