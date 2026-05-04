using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/HeadAttack3", fileName = "PlayerState_HeadAttack3")]
public class PlayerState_HeadAttack3 : IPlayerState
{
    [SerializeField] TimelineAsset _timeline;
    [SerializeField] float _attackDamage;
    [SerializeField] float _comboCharge;
    public override void EnterState()
    { 
        _director.playableAsset = _timeline;
        _director.time = 0;
        _director.Play();

        _player.ActionCancel = false;
        
        _player.AttackDataInput(_attackDamage,_comboCharge);
    }
    public override void ExitState()
    {
        _director.time = 0;
        _director.Stop();
    }
    public override void LogicUpdate()
    {
        if(_player.IsDie)
        {
            _stateMachine.SetState(typeof(PlayerState_Die));
        }
        if(_director.state != PlayState.Playing)
        { 
            _stateMachine.SetState(typeof(PlayerState_Idle));          
        }

        if(_input.IsDash && _player.ActionCancel)
        {
            _stateMachine.SetState(typeof(PlayerState_Dash));
        }
        
        if(_input.IsJump && _player.CanJump && _player.IsGrounded && _player.ActionCancel)
        {
            _stateMachine.SetState(typeof(PlayerState_Jump));
        }
        
        if(_input.IsRelease)
        {
            _stateMachine.SetState(typeof(PlayerState_Release));
        }
    }
    public override void PhysicsUpdate()
    { 
        
    }
}
