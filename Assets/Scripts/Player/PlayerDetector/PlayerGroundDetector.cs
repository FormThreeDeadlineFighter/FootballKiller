using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] float _detectionRadius = 0.1f;
    [SerializeField] LayerMask _groundLayer;
    Collider[] _colliders = new Collider[1];
    public bool IsGrounded => Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, _colliders, _groundLayer) != 0;

    void OnDrawGizmosSelected()
    {    
        Gizmos.color = Color.green;
        if(IsGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
