using UnityEngine;

public class PiState_PreAttack : IPiState
{
    public override void EnterState()
    {
        _enemy.AttackCD();
    }
    public override void ExitState()
    {
        
    }
    public override void LogicUpdate()
    {  
        int ran = Random.Range(1,4);
        switch(ran)
        {
            case 1: _stateMachine.SetState(typeof(PiState_HeavyAttack)); 
            break;
            case 2: _stateMachine.SetState(typeof(PiState_RoundAttack));
            break;
            case 3: _stateMachine.SetState(typeof(PiState_Summon));
            break;
        } 

    }
    public override void PhysicsUpdate()
    {
        
    }
}
