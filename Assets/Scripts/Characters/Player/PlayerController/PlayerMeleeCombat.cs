using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    [SerializeField] float _meleeRange;
    [SerializeField] LayerMask _layer;
    EnemyController currentTarget;
    PlayerInput _input;
    Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayertoEnemy(Vector3 inputDirection)
    {
        if(Physics.SphereCast(transform.position, _meleeRange, inputDirection, out RaycastHit info, 10, _layer))
        {
            if(info.collider.transform.GetComponent<EnemyController>())
            currentTarget = info.collider.transform.GetComponent<EnemyController>();
        }
    }

    void MovetowardsTarget(EnemyController target, float duration)
    {
        Quaternion toRotation = Quaternion.LookRotation(target.transform.position, Vector3.up);
        _rb.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
    }
}
