using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody), typeof(AISensor))]
public class BaseballStateController : MonoBehaviour
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    PlayableDirector director;
    [SerializeField] TimelineAsset[] timelineAssets;
    private float _timer = 5;
    private float _currentTime;
    void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        director = GetComponent<PlayableDirector>();
        
        _currentTime = 0;
    }

    private void FixedUpdate() 
    {
        if(_currentTime >= _timer)
        {
            int num = Random.Range(0,timelineAssets.Length);
            Debug.Log(num);     
            PlayerTimeline(timelineAssets[num]);
            _currentTime = 0;
            _enemy.SwitchElement();
            return;
        }
        if (director.state != PlayState.Playing)
        {           
            _enemy.FaceToPlayer();          
        }
        _currentTime += Time.fixedDeltaTime;
    }

    private void PlayerTimeline(TimelineAsset timeline)
    {
        if (director.state == PlayState.Playing) return;
        director.playableAsset = timeline;
        director.time = 0;
        director.Play();
    }
}
