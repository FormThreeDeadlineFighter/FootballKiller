using UnityEngine;

public class EnemyBullet : IBullet
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
