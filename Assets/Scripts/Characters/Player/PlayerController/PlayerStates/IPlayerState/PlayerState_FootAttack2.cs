using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FootAttack2", fileName = "PlayerState_FootAttack2")]
public class PlayerState_FootAttack2 : IPlayerState
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
                _stateMachine.SetState(typeof(PlayerState_FootAttack3));
            } 
            else
            {
                _stateMachine.SetState(typeof(PlayerState_Idle));
            }
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
