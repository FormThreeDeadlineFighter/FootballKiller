using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/BicycleKick", fileName = "PlayerState_BicycleKick")]
[System.Serializable]
public class PlayerState_BicycleKick : IPlayerState
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
        _player.PlayerShot(10, true); 
        Debug.Log("Player Bicycle kick");
    }
    public override void LogicUpdate()
    {   
        if(_currentTime >= _duration)
        {       
            _stateMachine.SetState(typeof(PlayerState_Land));
        }
        
        _currentTime += Time.deltaTime;
    }
    public override void PhysicsUpdate()
    {
        
    }
}
