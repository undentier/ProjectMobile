using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundsManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip openSound;
    public AudioClip backSound;
    public AudioClip startSound;
    public AudioClip slideSound;

    public static UISoundsManager uiSoundsManager;
    void Awake()
    {
        uiSoundsManager = this;
    }

    void Update()
    {
        
    }

    public void PlayOpenSound(float volume)
    {
        source.PlayOneShot(openSound, volume);
    }
    public void PlayBackSound(float volume)
    {
        source.PlayOneShot(backSound, volume);
    }
    public void PlayStartSound(float volume)
    {
        source.PlayOneShot(startSound, volume);
    }


    public void PlaySlideSound(float volume)
    {
        source.PlayOneShot(slideSound, volume);
    }
}
