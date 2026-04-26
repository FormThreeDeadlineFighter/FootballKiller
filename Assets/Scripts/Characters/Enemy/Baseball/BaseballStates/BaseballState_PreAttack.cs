using UnityEngine;

public class BaseballState_PreAttack : IBaseballState
{
    BaseballStateConfig_PreAttack _data;

    public BaseballState_PreAttack(BaseballStateConfig_PreAttack data) : base(data)
    {
        _data = data;
    }
    public override void EnterState()
    {
       _enemy.AttackCD();
    }
    public override void ExitState()
    {

    }
    public override void LogicUpdate()
    {
        int num = Random.Range(1, 4); 
        switch(num)
        {
            case 1: _stateMachine.SetState(typeof(BaseballState_Slash));
            break;
            case 2: _stateMachine.SetState(typeof(BaseballState_Collision));
            break;
            case 3: _stateMachine.SetState(typeof(BaseballState_Wave));
            break;
        }                
    }
    public override void PhysicsUpdate()
    {
        
    }
}
