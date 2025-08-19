using UnityEngine;

public class IBullet : MonoBehaviour
{
    Rigidbody _rb;
    private void OnEnable() 
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
        Destroy(this.gameObject, 3f);
    }
}
