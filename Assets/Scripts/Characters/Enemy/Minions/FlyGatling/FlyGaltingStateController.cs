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
    [SerializeField] StateConfig_FlyGalting _data;
    
    #region States
    FlyGaltingState_Die _flyGaltingState_Die;
    FlyGaltingState_Hurt _flyGaltingState_Hurt;
    FlyGaltingState_Idle _flyGaltingState_Idle;
    FlyGaltingState_PreAttack _flyGaltingState_PreAttack;
    FlyGaltingState_Shoot _flyGaltingState_Shoot;
    #endregion

    void OnEnable()
    {      
        _flyGaltingState_Die = new FlyGaltingState_Die();
        _flyGaltingState_Hurt = new FlyGaltingState_Hurt();
        _flyGaltingState_Idle = new FlyGaltingState_Idle();
        _flyGaltingState_PreAttack = new FlyGaltingState_PreAttack();
        _flyGaltingState_Shoot = new FlyGaltingState_Shoot();
    
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_shooterStates.Count); 
        
        _shooterStates.Add(_flyGaltingState_Die);
        _shooterStates.Add(_flyGaltingState_Hurt);
        _shooterStates.Add(_flyGaltingState_Idle);
        _shooterStates.Add(_flyGaltingState_PreAttack);
        _shooterStates.Add(_flyGaltingState_Shoot);
        
        foreach (IFlyGalting state in _shooterStates)
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
        SetState(_stateTable[typeof(FlyGaltingState_Idle)]);
    }
}
