using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/HeadAttack1", fileName = "PlayerState_HeadAttack1")]
public class PlayerState_HeadAttack1 : IPlayerState
{
    [SerializeField] TimelineAsset _timeline;
    [SerializeField] float _attackDamage;
    [SerializeField] float _comboCharge;
    private bool _preInput;
    public override void EnterState()
    { 
        _director.playableAsset = _timeline;
        _director.time = 0;
        _director.Play();
        
        _preInput = false;
        _player.ActionCancel = false;
        
        _player.AttackDataInput(_attackDamage, _comboCharge);
    }
    public override void ExitState()
    {
        _director.time = 0;
        _director.Stop();
    }
    public override void LogicUpdate()
    {
        if(_director.state != PlayState.Playing)
        {
            if(_preInput)
            {
                _stateMachine.SetState(typeof(PlayerState_HeadAttack2));
            } 
            else
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            }                
        }

        if(_input.IsDash && _player.ActionCancel)
        {
            _stateMachine.SetState(typeof(PlayerState_Dash));
        }
        if(_input.IsJump && _player.CanJump && _player.IsGrounded && _player.ActionCancel)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }

        if(_input.IsLightAttack)
        {
            _preInput = true;
        }   
        
        if(_input.IsRelease && _player.CurrentHoldGrade == HoldGrade.level0)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack1));
        }
        if(_input.IsRelease && _player.CurrentHoldGrade == HoldGrade.level1)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack2));
        }
        if(_input.IsRelease && _player.CurrentHoldGrade == HoldGrade.level2)
        {
            _stateMachine.SetState(typeof(PlayerState_FootAttack3));
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
