using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Shoot", fileName = "PlayerState_Shoot")]
[System.Serializable]
public class PlayerState_Shoot : IPlayerState
{
    [SerializeField] float _duration = 0.1f;
    private float _currentTime;
    public override void EnterState()
    { 
        base.EnterState();  
        _currentTime = 0;
    }
    public override void ExitState()
    {
        _player.PlayerShoot(); 
        Debug.Log("Player Shoot");
    }
    public override void LogicUpdate()
    {
        if(_currentTime >= _duration)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }
        
        _currentTime += Time.deltaTime;
    }
    public override void PhysicsUpdate()
    {
        
    }
}
