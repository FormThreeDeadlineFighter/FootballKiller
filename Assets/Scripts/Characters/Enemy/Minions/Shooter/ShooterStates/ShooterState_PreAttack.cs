using UnityEngine;

public class ShooterState_PreAttack : IShooterState
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
        int ran = Random.Range(1,3);
        switch(ran)
        {
            case 1: _stateMachine.SetState(typeof(ShooterState_Shoot)); 
            break;
            case 2: _stateMachine.SetState(typeof(ShooterState_Melee));
            break;
        } 
         

    }
    public override void PhysicsUpdate()
    {
       
    }
}
