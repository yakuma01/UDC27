using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : Singleton<SfxPlayer>
{
    [SerializeField] private AudioSource src;
    [SerializeField] public AudioClip[] audioClips;
    
    public void PlayAudioClip(AudioClip audio)
    {
        foreach(AudioClip currentaudio in audioClips)
        {
            if(currentaudio == audio)
            {
                src.clip = currentaudio;
                src.Play();
            }
        }
    }
}
