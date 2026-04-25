using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShooterStateController : IStateController
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector _director;  
    List<IShooter> _shooterStates = new List<IShooter>();
    
    #region States
    ShooterState_Idle _shooterState_Idle;
    ShooterState_Move _shooterState_Move;
    ShooterState_NormalShoot _shooterState_NormalShoot;
    ShooterState_SectorShoot _shooterState_SectorShoot;
    ShooterState_Hurt _shooterState_Hurt;
    ShooterState_Die _shooterState_Die;
    #endregion
    
    #region StateConfigs
    [SerializeField] ShooterStateConfig_Idle _idleData;
    [SerializeField] ShooterStateConfig_Move _moveData;
    [SerializeField] ShooterStateConfig_NormalShoot _normalShootData;
    [SerializeField] ShooterStateConfig_SectorShoot _sectorShootData;
    [SerializeField] ShooterStateConfig_Hurt _hurtData;
    [SerializeField] ShooterStateConfig_Die _dieData;
    #endregion
    void OnEnable()
    {      
        _shooterState_Idle = new ShooterState_Idle(_idleData);
        _shooterState_Move = new ShooterState_Move(_moveData);
        _shooterState_NormalShoot = new ShooterState_NormalShoot(_normalShootData);
        _shooterState_SectorShoot = new ShooterState_SectorShoot(_sectorShootData);
        _shooterState_Hurt = new ShooterState_Hurt(_hurtData);
        _shooterState_Die = new ShooterState_Die(_dieData);
        
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        _director = GetComponent<PlayableDirector>();
        
        _stateTable = new Dictionary<System.Type, IState>(_shooterStates.Count);  
        
        _shooterStates.Add(_shooterState_Idle);
        _shooterStates.Add(_shooterState_Move);
        _shooterStates.Add(_shooterState_NormalShoot);
        _shooterStates.Add(_shooterState_SectorShoot);
        _shooterStates.Add(_shooterState_Hurt);
        _shooterStates.Add(_shooterState_Die);
        
        foreach (IShooter state in _shooterStates)
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
        SetState(_stateTable[typeof(ShooterState_Idle)]);
    }
}
