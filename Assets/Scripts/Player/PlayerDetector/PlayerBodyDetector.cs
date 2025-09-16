using UnityEngine;

public class PlayerBodyDetector : MonoBehaviour
{
    [SerializeField] PlayerEvent _playerEvents;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            _playerEvents.PlayerHurt(attack.Damage);
        }
    }
}
