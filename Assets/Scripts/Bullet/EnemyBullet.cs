using UnityEngine;

public class EnemyBullet : IBullet
{
    [SerializeField] ParticleSystem boom;
    private void OnTriggerEnter(Collider other)
    {
        if(boom != null)
        {
            boom.Play();
        }
        
        Destroy(this.gameObject);
    }
}
