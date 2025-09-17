using UnityEngine;
using UnityEngine.Events;

public class PlayerBodyDetector : MonoBehaviour
{
    [SerializeField] PlayerEvent _playerEvents;
    private float damage;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            damage = attack.Damage;
            _playerEvents.PlayerHurt(attack.Damage);
        }
    }
}
