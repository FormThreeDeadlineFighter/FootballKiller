using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] AudioClip _SFX;
    [SerializeField, Range(0f,1f)] float _volume;
    public void OnPlay()
    {
        AudioManager.Instance.PlaySFX(_SFX, _volume);
    }
}
