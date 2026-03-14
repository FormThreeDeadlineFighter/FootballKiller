using UnityEngine;

public class BaseballController : MonoBehaviour
{
    [SerializeField] float _collisionforwardDistance;
    [SerializeField] ParticleSystem _onHitEffect;
    [SerializeField] BossEvent _bossEvent;

    void OnEnable()
    {
        _bossEvent.OnBossHPCahange += OnHurtEffect;
    }
    void OnDisable()
    {
        _bossEvent.OnBossHPCahange -= OnHurtEffect;
    }
    public void CollisionMoveTrigger()
    {
        transform.position = transform.position + transform.forward * _collisionforwardDistance;
    }
    
    private void OnHurtEffect(float value)
    {
        _onHitEffect.Play();
    }
}
