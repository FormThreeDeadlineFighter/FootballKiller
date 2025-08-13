using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip shoot;
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
        for (int i = 0; i < 3; i++)
        {
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }
    }
    void Play(int index, string name,bool isloop)
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
            case "_shoot":
                return shoot;
        }
        return null;
    }
}
