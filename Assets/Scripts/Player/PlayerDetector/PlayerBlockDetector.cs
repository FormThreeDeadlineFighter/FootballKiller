using UnityEngine;
using UnityEngine.Events;

public class PlayerBlockDetector : MonoBehaviour
{
    public float AttackDamage;
    [SerializeField] PlayerEvent _playerEvents;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            AttackDamage = attack.Damage;
            _playerEvents.PlayerBlock(attack.Elements);
            Debug.Log("player block");
        }
    }
    void OnTriggerExit(Collider other)
    {
        _playerEvents.PlayerBlock(Elements.none);
    }
}
