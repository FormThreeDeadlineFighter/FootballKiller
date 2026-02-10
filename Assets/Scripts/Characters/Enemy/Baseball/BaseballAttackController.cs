using UnityEngine;

public class BaseballAttackController : MonoBehaviour
{
    [SerializeField] float _collisionforwardDistance;

    public void CollisionMoveTrigger()
    {
        transform.position = transform.position + transform.forward * _collisionforwardDistance;
    }
}
