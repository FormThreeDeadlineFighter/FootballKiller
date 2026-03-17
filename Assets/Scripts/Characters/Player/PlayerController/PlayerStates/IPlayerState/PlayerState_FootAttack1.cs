using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FootAttack1", fileName = "PlayerState_FootAttack1")]
public class PlayerState_FootAttack1 : IPlayerState
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
                _stateMachine.SetState(typeof(PlayerState_FootAttack2));
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
        
        if(_input.IsHeavyAttack)
        {
            _preInput = true;
        } 
    }
    public override void PhysicsUpdate()
    { 

    }
}
