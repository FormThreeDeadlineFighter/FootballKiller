using UnityEngine;

public class PlayerBullet : IBullet
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
