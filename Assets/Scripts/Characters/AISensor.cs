using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] float _detectionRadius;
    [SerializeField] LayerMask _layer;
    private Collider[] _colliders = new Collider[100];
    [HideInInspector] public Collider Target;

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, _colliders, _layer);
        if (_colliders[0] == null) return;
        Vector3 dir = _colliders[0].transform.position - transform.position;
        
        if(Physics.Raycast(transform.position, dir, out RaycastHit hit,_detectionRadius,_layer))
        {
            Target = hit.collider;
            Vector3 dir2 = Target.transform.position - transform.position;
            Debug.DrawRay(transform.position, dir2, Color.yellow); 
        }              
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
