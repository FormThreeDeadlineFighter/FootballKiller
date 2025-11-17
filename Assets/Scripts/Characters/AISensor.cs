using UnityEngine;

public class AISensor : MonoBehaviour
{
    [SerializeField] float _detectionRadius;
    [SerializeField] LayerMask _layer;
    [SerializeField] Transform _startPosition;
    private Collider[] _colliders = new Collider[100];
    [HideInInspector] public Collider Target;

    private void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(_startPosition.transform.position, _detectionRadius, _colliders, _layer);
        if (_colliders[0] == null) return;
        Vector3 dir = _colliders[0].transform.position - _startPosition.transform.position;
        
        if(Physics.Raycast(_startPosition.transform.position, dir, out RaycastHit hit,_detectionRadius,_layer))
        {
            Target = hit.collider;
            Vector3 dir2 = Target.transform.position - _startPosition.transform.position;
            Debug.DrawRay(_startPosition.transform.position, dir2, Color.yellow); 
        }              
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
