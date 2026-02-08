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
    
    #region States
    BaseballState_BackJump _baseballState_BackJump; 
    BaseballState_Collision _baseballState_Collision; 
    BaseballState_Forward _baseballState_Forward; 
    BaseballState_Idle _baseballState_Idle; 
    BaseballState_PreAttack _baseballState_PreAttack;
    BaseballState_PreMove _baseballState_PreMove;
    BaseballState_SideStep _baseballState_SideStep;
    BaseballState_Slash _baseballState_Slash;
    BaseballState_Wave _baseballState_Wave;
    #endregion
    
    #region StateConfigs
    [SerializeField] BaseballStateConfig_BackJump _backJumpData;
    [SerializeField] BaseballStateConfig_Collision _collisionData;
    [SerializeField] BaseballStateConfig_Forward _forwardData;
    [SerializeField] BaseballStateConfig_Idle _idleData;
    [SerializeField] BaseballStateConfig_PreAttack _preAttackData;
    [SerializeField] BaseballStateConfig_PreMove _preMoveData;
    [SerializeField] BaseballStateConfig_SideStep _sideStepData;
    [SerializeField] BaseballStateConfig_Slash _slashData;
    [SerializeField] BaseballStateConfig_Wave _waveData;
    #endregion
    void OnEnable()
    {
        _baseballState_BackJump = new BaseballState_BackJump(_backJumpData);
        _baseballState_Collision = new BaseballState_Collision(_collisionData);
        _baseballState_Forward = new BaseballState_Forward(_forwardData);
        _baseballState_Idle = new BaseballState_Idle(_idleData);
        _baseballState_PreAttack = new BaseballState_PreAttack(_preAttackData);
        _baseballState_PreMove = new BaseballState_PreMove(_preMoveData);
        _baseballState_SideStep = new BaseballState_SideStep(_sideStepData);
        _baseballState_Slash = new BaseballState_Slash(_slashData);
        _baseballState_Wave = new BaseballState_Wave(_waveData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_baseballStates.Count);
        
        _baseballStates.Add(_baseballState_Idle);
        
        foreach (IBaseballState state in _baseballStates)
        {
            state.Initialize(this, _enemy, _animator, _director);
            _stateTable.Add(state.GetType(), state);
        }
         
    }
    
    void OnDisable()
    {
        _stateTable.Clear();
    }
    
    void Start()
    {
        SetState(_stateTable[typeof(BaseballState_Idle)]);
    }


    /*private void FixedUpdate() 
    {
        if(_currentTime >= _timer)
        {
            int num = Random.Range(0,timelineAssets.Length); 
            PlayerTimeline(timelineAssets[num]);
            _currentTime = 0;
            _enemy.SwitchElement();
            return;
        }
        if (director.state != PlayState.Playing)
        {           
            _enemy.FaceToPlayer();          
        }
        _currentTime += Time.fixedDeltaTime;
    }
    private void PlayerTimeline(TimelineAsset timeline)
    {
        if (_director.state == PlayState.Playing) return;
        _director.playableAsset = timeline;
        _director.time = 0;
        _director.Play();
    }*/
}
