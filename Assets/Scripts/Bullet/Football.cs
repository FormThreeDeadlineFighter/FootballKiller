using UnityEngine;

public class Football : MonoBehaviour
{
    [SerializeField] public bool _attack;
    [SerializeField] private GameObject energyBox;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {         
            Instantiate(energyBox, transform.position, Quaternion.identity);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}
