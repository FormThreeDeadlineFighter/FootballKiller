using UnityEngine;

public class Football : MonoBehaviour
{
    [SerializeField] public bool _attack;
    [SerializeField] private GameObject energyBox;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {         
            Debug.Log("boom");
            Instantiate(energyBox, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}
