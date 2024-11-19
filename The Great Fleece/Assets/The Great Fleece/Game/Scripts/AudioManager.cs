using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Audio Manager is Null!");
            }
            return instance;
        }

    }

    public AudioSource voiceOver;
    public AudioSource music;

    private void Awake()
    {
        instance = this;
    }

    public void PlayVoiceOver(AudioClip clipToPlay)
    {
        voiceOver.clip = clipToPlay;
        voiceOver.Play();
    }

    public void PlayMusic()
    {
        music.Play();
    }
}
