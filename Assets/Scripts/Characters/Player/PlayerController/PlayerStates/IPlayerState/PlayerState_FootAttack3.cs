using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/FootAttack3", fileName = "PlayerState_FootAttack3")]
public class PlayerState_FootAttack3 : IPlayerState
{
    [SerializeField] TimelineAsset _timeline;
    [SerializeField] float _attackDamage;
    [SerializeField] float _comboCharge;
    public override void EnterState()
    { 
        _director.playableAsset = _timeline;
        _director.time = 0;
        _director.Play();
        
        _player.AttackDataInput(_attackDamage,_comboCharge);
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
            _stateMachine.SetState(typeof(PlayerState_Idle));          
        }
    }
    public override void PhysicsUpdate()
    { 

    }
}
