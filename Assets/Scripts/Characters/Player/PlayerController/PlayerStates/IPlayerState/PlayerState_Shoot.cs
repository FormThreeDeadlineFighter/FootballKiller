using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Shoot", fileName = "PlayerState_Shoot")]
[System.Serializable]
public class PlayerState_Shoot : IPlayerState
{
    private float _currentTime;
    public override void EnterState()
    {     
        base.EnterState();
        _player.FaceToEnemy();
        _currentTime = 0;
    }
    public override void ExitState()
    {
        if((int)_currentTime == 0)
        {        
            _player.PlayerShoot(5); 
        }
        else
        {
            _player.PlayerShoot((int)_currentTime * 25); 
        }
        Debug.Log("Player Shot");
    }
    public override void LogicUpdate()
    {
        if(IsAnimationComplete && !_input.IsPlayerShoot)
        {      
            _stateMachine.SetState(typeof(PlayerState_Idle));  
        }
        
        _currentTime += Time.deltaTime;
    }
    public override void PhysicsUpdate()
    {
        
    }
}
