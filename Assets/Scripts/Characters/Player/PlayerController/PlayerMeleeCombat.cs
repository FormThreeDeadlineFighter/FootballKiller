using System.Collections;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    [SerializeField] float _meleeRange;
    [SerializeField] float angle = 30f; // 扇形角度
    [SerializeField] int rayCount = 5;  // Ray 數量（越多越準）
    [SerializeField] float stopRadius = 1.5f; // 停在敵人前位置
    [SerializeField] LayerMask _layer;
    private Collider[] _colliders = new Collider[50];
    Transform currentTarget;
    Coroutine moveToTargetCoroutine;

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

    public void MoveTowardsTarget()
    {
        if(currentTarget == null) return;
  
        float distance = Vector3.Distance(currentTarget.transform.position, transform.position);
        if(distance < stopRadius)return;
        
        // Do Move   
        if(moveToTargetCoroutine != null)
        {
            StopCoroutine(moveToTargetCoroutine);
        }
        
        moveToTargetCoroutine = StartCoroutine(MoveToTarget(currentTarget.transform, stopRadius));
    }
    
    IEnumerator MoveToTarget(Transform enemy, float stopRadius)
    {
        float duration = 0.15f;   // Arkham 常用 0.1 ~ 0.2
        float elapsed = 0f;

        Vector3 startPos = transform.position;
        
        // 計算「一定合法」的終點
        Vector3 dir = (startPos - enemy.position).normalized;
        dir.y = 0;

        Vector3 targetPos = enemy.position + dir * stopRadius;
        
        // 防止玩家上下動
        targetPos.y = transform.position.y;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 平滑（不要線性）
            t = Mathf.SmoothStep(0, 1, t);

            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        // 保證最後停在正確位置
        transform.position = targetPos;
        
        yield return null;
        moveToTargetCoroutine = null;
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
