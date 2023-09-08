using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip[] audioClips;
    
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
