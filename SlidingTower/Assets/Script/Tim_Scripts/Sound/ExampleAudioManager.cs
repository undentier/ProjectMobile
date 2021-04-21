using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioManager : MonoBehaviour
{

    //AudioSource
    //Insert AudioSource

    //Audio mixer Groups
    //insert audiomixergroup

    //Sounds
    //insert audioclip



    public void PlayClip(AudioSource source, AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        source.outputAudioMixerGroup = mixer;
        source.pitch = pitch;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }

    public void PlayClipNat(AudioSource source, AudioClip clip, float volume, AudioMixerGroup mixer)
    {
        source.outputAudioMixerGroup = mixer;
        source.pitch = 1;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }
}