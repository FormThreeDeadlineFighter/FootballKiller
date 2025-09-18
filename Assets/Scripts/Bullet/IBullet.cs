using UnityEngine;

public class IBullet : MonoBehaviour
{
    [SerializeField] float _speed = 100f;
    Rigidbody _rb;
    private void OnEnable() 
    {
        // bullet speed;
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
