using UnityEngine;
using UnityEngine.Events;

public class PlayerBlockDetector : MonoBehaviour
{
    Collider[] _colliders = new Collider[50];
    public Elements _elementsBlock;
    public float AttackDamage;

    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IAttack>(out IAttack attack))
        {
            _elementsBlock = attack.Elements;
            AttackDamage = attack.Damage;
        }
    }
    void OnTriggerExit(Collider other)
    {
        _elementsBlock = Elements.none;
    }
}
