using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [SerializeField] AudioClip _music;
    [SerializeField, Range(0f,1f)] float _volume;
    
    [SerializeField] bool IsPlayOnStart;
    void Start()
    {
        if(IsPlayOnStart)
        {      
            AudioManager.Instance.PlayBGM(_music, _volume);
        }
    }

    void OnEnable()
    {
        AudioManager.Instance.PlayBGM(_music, _volume);
    }
}
