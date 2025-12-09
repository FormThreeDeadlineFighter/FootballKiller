using UnityEngine;
using System.Collections;

public class Football : MonoBehaviour
{
    public bool _attack;
    [SerializeField] private GameObject energyBox;
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {         
            Debug.Log("boom");
            Instantiate(energyBox, transform.position, Quaternion.identity);
        }
        //Destroy(this.gameObject);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {         
            Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
    
    void Stop()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity =  Vector3.zero;
    }
}
