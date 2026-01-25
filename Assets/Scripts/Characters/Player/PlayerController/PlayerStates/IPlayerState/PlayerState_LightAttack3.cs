using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/LightAttack3", fileName = "PlayerState_LightAttack3")]
public class PlayerState_LightAttack3 : IPlayerState
{
    [SerializeField] float _attackDamage;
    [SerializeField] float _comboCharge;
    public override void EnterState()
    { 
        base.EnterState();
        
        _player.AttackEnter(_attackDamage,_comboCharge);
    }
    public override void ExitState()
    {
        _player.AttackExit();
    }
    public override void LogicUpdate()
    {
        if(IsAnimationComplete)
        { 
            _stateMachine.SetState(typeof(PlayerState_Idle));          
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
