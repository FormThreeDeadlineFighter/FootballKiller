using UnityEngine;

public class PlayerBullet : IBullet
{
    [SerializeField] ParticleSystem boom;
    private void OnTriggerEnter(Collider other)
    {
        if(boom != null)
        {
            boom.Play();
            Debug.Log("boom");
        }
        
        Destroy(this.gameObject);
    }
}
