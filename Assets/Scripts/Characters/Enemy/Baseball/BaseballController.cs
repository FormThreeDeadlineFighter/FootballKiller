using UnityEngine;

public class BaseballController : MonoBehaviour
{
    [SerializeField] float _collisionforwardDistance;
    [SerializeField] BossEvent _bossEvent;

    void OnEnable()
    {
       
    }
    void OnDisable()
    {
        
    }
    public void CollisionMoveTrigger()
    {
        transform.position = transform.position + transform.forward * _collisionforwardDistance;
    }
}
