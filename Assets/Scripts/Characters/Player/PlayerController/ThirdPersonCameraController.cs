using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] Transform currentTarget;
    [SerializeField] float lockDistance = 10f;
    [SerializeField] LayerMask _layer;
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] PlayerEvent playerEvent;
    private Collider[] _colliders = new Collider[50];

    void OnEnable()
    {
        playerEvent.OnLock += OnLock;
    }
    void OnDisable()
    {
        playerEvent.OnLock -= OnLock;
    }
    
    void OnLock()
    {
        if (currentTarget == null)
            {         
                Debug.Log("tag check"); 
                LockEnemy();    
            }
            else
            {
                UnlockEnemy();
            }
    }
    
    void LockEnemy()
    {
        if(Physics.OverlapSphereNonAlloc(transform.position, lockDistance, _colliders, _layer) != 0)
        {                  
            currentTarget = _colliders[0].transform; 
            targetGroup.AddMember(currentTarget, 1, 2);
        }
    }  
    

    public void UnlockEnemy()
    {
        if(currentTarget != null)
        {
            targetGroup.RemoveMember(currentTarget);

            currentTarget = null;
        }
    } 
}
