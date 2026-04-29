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
    [SerializeField] StateConfig_Tank _data;
    
    #region States
    TankState_Beat _tankState_Beat;
    TankState_Charge _tankState_Charge;
    TankState_Die _tankState_Die;
    TankState_Hurt _tankState_Hurt;
    TankState_Idle _tankState_Idle;
    TankState_PreAttack _tankState_PreAttack;
    TankState_Shoot _tankState_Shoot;
    #endregion
    
    void OnEnable()
    {      
        _tankState_Beat = new TankState_Beat();
        _tankState_Charge = new TankState_Charge();
        _tankState_Die = new TankState_Die();
        _tankState_Hurt = new TankState_Hurt();
        _tankState_Idle = new TankState_Idle();
        _tankState_PreAttack = new TankState_PreAttack();
        _tankState_Shoot = new TankState_Shoot();
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_tankStates.Count); 
        
        _tankStates.Add(_tankState_Beat);
        _tankStates.Add(_tankState_Charge);
        _tankStates.Add(_tankState_Die);
        _tankStates.Add(_tankState_Hurt);
        _tankStates.Add(_tankState_Idle);
        _tankStates.Add(_tankState_PreAttack);
        _tankStates.Add(_tankState_Shoot);
    
        foreach (ITank state in _tankStates)
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
        SetState(_stateTable[typeof(TankState_Idle)]);
    }
}
