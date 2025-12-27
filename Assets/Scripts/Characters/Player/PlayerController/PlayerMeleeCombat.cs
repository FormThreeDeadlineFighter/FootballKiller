using System.Collections;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    [SerializeField] float _meleeRange;
    [SerializeField] float angle = 30f; // 扇形角度
    [SerializeField] int rayCount = 5;  // Ray 數量（越多越準）
    [SerializeField] LayerMask _layer;
    private Collider[] _colliders = new Collider[50];
    Transform currentTarget;

    public Transform SelectTarget()
    {
        if(Physics.OverlapSphereNonAlloc(transform.position, _meleeRange, _colliders, _layer) != 0)
        {
            for(int i = 0; i< rayCount;i++)
            {
                Vector3 dir = GetRayDirection(i);
                
                if(Physics.Raycast(transform.position, dir, out RaycastHit hit, _meleeRange, _layer))
                {              
                    currentTarget = hit.collider.transform;
                    Debug.Log("find enemy");
                    return currentTarget;
                }
            }
            
        }
        currentTarget = null;
        return currentTarget;
    }
    
    Vector3 GetRayDirection(int index)
    {    
        float step = angle / (rayCount - 1);
        float startAngle = -angle * 0.5f;
        float current = startAngle + step * index;

        return Quaternion.Euler(0, current, 0) * transform.forward;
    }

    public void MoveTowardsTarget(Rigidbody rb)
    {
        if(currentTarget == null) return;
        // Do Loot At
        Quaternion toRotation = Quaternion.LookRotation(currentTarget.transform.position, Vector3.up);
        rb.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
        
        // Do Move
        StartCoroutine(MoveToTarget(currentTarget.transform));
    }
    
    IEnumerator MoveToTarget(Transform target)
    {
        Vector3 start = transform.position;
        Vector3 end = target.position - target.forward * 0.8f;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            transform.position = Vector3.Lerp(start, end, t);
            transform.LookAt(target);
            yield return null;
        }
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _meleeRange);
        
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 dir = GetRayDirection(i);
            Gizmos.DrawLine(transform.position, transform.position + dir * _meleeRange);
        }
        
        Gizmos.color = Color.blue;
        
        if(currentTarget != null)
        {         
            Gizmos.DrawSphere(currentTarget.transform.position, 1f);
        }
    }
}
