using UnityEngine;
using UnityEngine.Events;

public class PlayerBlockDetector : MonoBehaviour
{
    [SerializeField] Vector3 _halfExtents;
    [SerializeField] LayerMask _energyLayer;
    Collider[] _colliders = new Collider[50];
    public Elements _elementsBlock;
    public float AttackDamage;
    public bool IsBlock
    {
        get
        {
            if (Physics.OverlapBoxNonAlloc(transform.position, _halfExtents, _colliders, Quaternion.identity, _energyLayer) != 0)
            {
                if(_colliders[0].gameObject.TryGetComponent<IAttack>(out IAttack attack))
                {
                    _elementsBlock = attack.Elements;
                    AttackDamage = attack.Damage;
                }
                return true;
            }
            else
            {
                _elementsBlock = Elements.none;
                return false;
            }
        }
        
    }

    void OnEnable()
    {
        
    }
    void OnDisable()
    {
        
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if(IsBlock)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position, _halfExtents * 2);
    }
}
