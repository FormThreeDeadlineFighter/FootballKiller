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
    [SerializeField] BaseballState_IdleConfig idledata;
    BaseballState_Idle baseballState_Idle; 
    void OnEnable()
    {
        baseballState_Idle = new BaseballState_Idle(idledata);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_baseballStates.Count);
        
        _baseballStates.Add(baseballState_Idle);
        
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
