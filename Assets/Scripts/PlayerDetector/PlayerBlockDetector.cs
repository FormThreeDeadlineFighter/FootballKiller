using UnityEngine;

public class PlayerBlockDetector : MonoBehaviour
{
    [SerializeField] Vector3 _halfExtents;
    [SerializeField] LayerMask _energyLayer;
    Collider[] _colliders = new Collider[1];
    public bool IsBlock;
    
    private void OnBlock()
    {
        Physics.OverlapBoxNonAlloc(transform.position, _halfExtents, _colliders, Quaternion.identity, _energyLayer);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _halfExtents);
    }
}
