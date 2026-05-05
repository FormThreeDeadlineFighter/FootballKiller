using UnityEngine;
using UnityEngine.VFX;

public class VFXTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _vfx;
    
    public void PlayVFX()
    {
        _vfx.Play();
    }
    public void StopVFX()
    {
        _vfx.Stop();
    }
}
