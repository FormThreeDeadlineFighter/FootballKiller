using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/LightAttack1", fileName = "PlayerState_LightAttack1")]
public class PlayerState_LightAttack1 : IPlayerState
{
    [SerializeField] float _attackDamage;
    [SerializeField] float _comboCharge;
    private bool _preInput;
    public override void EnterState()
    { 
        base.EnterState();
        _preInput = false;
        
        _player.AttackEnter(_attackDamage, _comboCharge);
    }
    public override void ExitState()
    {
        _player.AttackExit();
    }
    public override void LogicUpdate()
    {
        if(IsAnimationComplete)
        {
            if(_preInput)
            {
                _stateMachine.SetState(typeof(PlayerState_LightAttack2));
            }  
            else
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            }               
        }
        
        if(_input.IsLightAttack)
        {
            _preInput = true;
        } 
    }
    public override void PhysicsUpdate()
    { 

    }
}
