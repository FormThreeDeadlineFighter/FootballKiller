using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody), typeof(AISensor))]
public class BaseballStateController : MonoBehaviour
{
    // enemy animator
    private Animator _animator;
    // enemy controller
    private EnemyController _enemy;
    [SerializeField] PlayableDirector Attack1;
    [SerializeField] PlayableDirector Attack2;
    private float _timer = 5;
    private float _currentTime;
    void OnEnable()
    {
        _enemy = GetComponent<EnemyController>();
        _animator = GetComponentInChildren<Animator>();
        
        _currentTime = _timer;
    }

    private void FixedUpdate() 
    {
        if(_currentTime < 0)
        {
            float num = Random.Range(1,3);
            Debug.Log(num);
            switch(num)
            {
                case 1: Attack1.Play();
                break;
                case 2: Attack2.Play();
                break;
                default:
                break;
            }
            _currentTime = _timer;
        }
        _currentTime -= Time.fixedDeltaTime;
    }

}
