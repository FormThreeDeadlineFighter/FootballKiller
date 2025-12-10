using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Retrieve", fileName = "PlayerState_Retrieve")]
[System.Serializable]
public class PlayerState_Retrieve : IPlayerState
{
    [SerializeField] float _retrieveTime = 0.3f;
    private float currentTime;
    public override void EnterState()
    { 
        base.EnterState();
        
        currentTime = 0;
        _player.Retrieve(_retrieveTime);
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        if(currentTime >= _retrieveTime &&_player.MoveMode == MoveMode.idle)
        {
            _stateMachine.SetState(typeof(PlayerState_Idle));
        }      
        
        currentTime += Time.deltaTime;
    }
    public override void PhysicsUpdate()
    {
        
    }
}
