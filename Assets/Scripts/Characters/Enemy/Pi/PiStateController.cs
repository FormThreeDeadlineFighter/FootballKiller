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
    PiState_Die _piState_Die;
    PiState_HeavyAttack _piState_HeavyAttack;
    PiState_Hurt _piState_Hurt;
    PiState_Idle _piState_Idle;
    PiState_Move _piState_Move;
    PiState_PreAttack _piState_PreAttack;
    PiState_RoundAttack _piState_RoundAttack;
    PiState_Summon _piState_Summon;
    #endregion

    void OnEnable()
    {
        _piState_Die = new PiState_Die();
        _piState_HeavyAttack = new PiState_HeavyAttack();
        _piState_Hurt = new PiState_Hurt();
        _piState_Idle = new PiState_Idle();
        _piState_Move = new PiState_Move();
        _piState_PreAttack = new PiState_PreAttack();
        _piState_RoundAttack = new PiState_RoundAttack();
        _piState_Summon = new PiState_Summon();
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_piStates.Count);
        
        _piStates.Add(_piState_Die);
        _piStates.Add(_piState_HeavyAttack);
        _piStates.Add(_piState_Hurt);
        _piStates.Add(_piState_Idle);
        _piStates.Add(_piState_Move);
        _piStates.Add(_piState_PreAttack);
        _piStates.Add(_piState_RoundAttack);
        _piStates.Add(_piState_Summon);
        
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
        SetState(_stateTable[typeof(PiState_Idle)]);
    }
    
}
