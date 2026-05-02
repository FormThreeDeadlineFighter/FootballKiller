using UnityEngine;

public class FlyGaltingState_PreAttack : IFlyGalting
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
        _stateMachine.SetState(typeof(FlyGaltingState_Shoot));      
    }
    public override void PhysicsUpdate()
    {
        
    }
}
