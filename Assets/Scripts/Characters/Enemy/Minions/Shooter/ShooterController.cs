using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public void Shoot()
    {
        Instantiate(bullet);
    }
}
