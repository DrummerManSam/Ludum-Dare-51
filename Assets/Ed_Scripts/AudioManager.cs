using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource aSource;

    public void Awake()
    {
        instance = this;
        aSource = GetComponent<AudioSource>();
    }


    public void PlayAudio(AudioClip clip)
    {
        aSource.PlayOneShot(clip);
    }
}
