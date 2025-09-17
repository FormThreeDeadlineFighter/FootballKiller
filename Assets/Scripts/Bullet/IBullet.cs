using UnityEngine;

public class IBullet : MonoBehaviour
{
    Rigidbody _rb;
    Renderer _renderer;
    private void OnEnable() 
    {
        // bullet speed;
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
