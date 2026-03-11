using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody), typeof(AISensor))]
public class BaseballStateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;
    List<IBaseballState> _baseballStates = new List<IBaseballState>();
    [SerializeField] BossEvent _bossEvent;
    
    #region States
    BaseballState_BackJump _baseballState_BackJump; 
    BaseballState_Collision _baseballState_Collision; 
    BaseballState_Forward _baseballState_Forward; 
    BaseballState_Idle _baseballState_Idle; 
    BaseballState_PreAttack _baseballState_PreAttack;
    BaseballState_SideStep _baseballState_SideStep;
    BaseballState_Slash _baseballState_Slash;
    BaseballState_Wave _baseballState_Wave;
    BaseballState_Hurt _baseballState_Hurt;
    BaseballState_Die _baseballState_Die;
    #endregion
    
    #region StateConfigs
    [SerializeField] BaseballStateConfig_BackJump _backJumpData;
    [SerializeField] BaseballStateConfig_Collision _collisionData;
    [SerializeField] BaseballStateConfig_Forward _forwardData;
    [SerializeField] BaseballStateConfig_Idle _idleData;
    [SerializeField] BaseballStateConfig_PreAttack _preAttackData;
    [SerializeField] BaseballStateConfig_SideStep _sideStepData;
    [SerializeField] BaseballStateConfig_Slash _slashData;
    [SerializeField] BaseballStateConfig_Wave _waveData;
    [SerializeField] BaseballStateConfig_Hurt _hurtData;
    [SerializeField] BaseballStateConfig_Die _dieData;
    #endregion
    void OnEnable()
    {
        _baseballState_BackJump = new BaseballState_BackJump(_backJumpData);
        _baseballState_Collision = new BaseballState_Collision(_collisionData);
        _baseballState_Forward = new BaseballState_Forward(_forwardData);
        _baseballState_Idle = new BaseballState_Idle(_idleData);
        _baseballState_PreAttack = new BaseballState_PreAttack(_preAttackData);
        _baseballState_SideStep = new BaseballState_SideStep(_sideStepData);
        _baseballState_Slash = new BaseballState_Slash(_slashData);
        _baseballState_Wave = new BaseballState_Wave(_waveData);
        _baseballState_Hurt = new BaseballState_Hurt(_hurtData);
        _baseballState_Die = new BaseballState_Die(_dieData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_baseballStates.Count);
        
        _baseballStates.Add(_baseballState_BackJump);
        _baseballStates.Add(_baseballState_Collision);
        _baseballStates.Add(_baseballState_Forward);
        _baseballStates.Add(_baseballState_Idle);
        _baseballStates.Add(_baseballState_PreAttack);
        _baseballStates.Add(_baseballState_SideStep);
        _baseballStates.Add(_baseballState_Slash);
        _baseballStates.Add(_baseballState_Wave);
        _baseballStates.Add(_baseballState_Hurt);
        _baseballStates.Add(_baseballState_Die);
        
        foreach (IBaseballState state in _baseballStates)
        {
            state.Initialize(this, _enemy, _animator, _director);
            _stateTable.Add(state.GetType(), state);
        }
        
        _bossEvent.OnBossDie += OnDie;
    }
    
    void OnDisable()
    {
        _stateTable.Clear();
        _bossEvent.OnBossDie -= OnDie;
    }
    
    void Start()
    {
        SetState(_stateTable[typeof(BaseballState_Idle)]);
    }
    
    void OnDie()
    {
        SetState(_stateTable[typeof(BaseballState_Die)]);
    }
}
