using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    main,
    battle,
    baseball
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sFXPlayer;
    [SerializeField] AudioSource bGMPlayer;
    private AudioClip _currentBGM;
    public static AudioManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }  
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlaySFX(AudioClip audioClip, float volume)
    {
        sFXPlayer.PlayOneShot(audioClip, volume);
    }
    public void PlayBGM(AudioClip audioClip, float volume)
    {
        if(_currentBGM == audioClip) return;        
        _currentBGM = audioClip;
        bGMPlayer.clip = audioClip;
        bGMPlayer.volume = volume;
        bGMPlayer.Play();
    }
}
