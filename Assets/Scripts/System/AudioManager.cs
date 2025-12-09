using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip click;
    public AudioClip shoot;
    public AudioClip bgm_mianground; 
    public AudioClip bgm_boss_1;
    List<AudioSource> audios = new List<AudioSource>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }  
        for (int i = 0; i < 4; i++)
        {
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }
    }
    public void Play(int index, string name,bool isloop)
    {
        var clip = GetAudioClip(name);
        if (clip != null)
        {
            var audio = audios[index];
            audio.clip = clip;
            audio.loop = isloop;
            audio.Play();
        }
    }
    AudioClip GetAudioClip(string name)
    {
        switch (name)
        {
            case "click":
                return click;
            case "shoot":
                return shoot;
            case "bgm_mianground":
                return bgm_mianground;
            case "bgm_boss_1":
                return bgm_boss_1;
        }
        return null;
    }
}
