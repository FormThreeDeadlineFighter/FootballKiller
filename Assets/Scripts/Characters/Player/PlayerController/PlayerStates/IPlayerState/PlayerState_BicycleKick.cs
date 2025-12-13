using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/BicycleKick", fileName = "PlayerState_BicycleKick")]
[System.Serializable]
public class PlayerState_BicycleKick : IPlayerState
{
    public override void EnterState()
    { 
        base.EnterState();
        _player.FaceToEnemy();
    }
    public override void ExitState()
    {
        _player.PlayerShot(10, true); 
        Debug.Log("Player Bicycle kick");
    }
    public override void LogicUpdate()
    {   
        if(IsAnimationComplete)
        {       
            _stateMachine.SetState(typeof(PlayerState_Land));
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
