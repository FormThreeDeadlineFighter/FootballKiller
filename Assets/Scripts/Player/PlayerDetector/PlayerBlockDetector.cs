using UnityEngine;
using UnityEngine.Events;

public class PlayerBlockDetector : MonoBehaviour
{
    Collider[] _colliders = new Collider[50];
    public Elements _elementsBlock;
    public float AttackDamage;
    [SerializeField] PlayerEvent _playerEvents;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            _elementsBlock = attack.Elements;
            AttackDamage = attack.Damage;
            _playerEvents.PlayerBlock(_elementsBlock);
        }
    }
    void OnTriggerExit(Collider other)
    {
        _elementsBlock = Elements.none;
    }
}
