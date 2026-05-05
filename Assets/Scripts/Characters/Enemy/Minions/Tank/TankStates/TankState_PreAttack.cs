using UnityEngine;

public class TankState_PreAttack : ITankState
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
            case 1: _stateMachine.SetState(typeof(TankState_Beat)); 
            break;
            case 2: _stateMachine.SetState(typeof(TankState_Charge));
            break;
            case 3: _stateMachine.SetState(typeof(TankState_Shoot));
            break;
        }
    }
    public override void PhysicsUpdate()
    {
        
    }
}
