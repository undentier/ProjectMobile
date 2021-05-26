using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip startWaveSound;
    public AudioClip okSound;
    public AudioClip selectTurret;
    public AudioClip unselectTurret;
    public AudioClip nexusDamagedSound;
    public AudioClip buildModeOpenSound;

    public static PlayerSoundManager I;
    void Awake()
    {
        I = this;
    }

    public void StartWave(float volume)
    {
        source.PlayOneShot(startWaveSound, volume);
    }
    public void OK(float volume)
    {
        source.PlayOneShot(okSound, volume);
    }
    public void SelectTurret(float volume)
    {
        source.PlayOneShot(selectTurret, volume);
    }
    public void UnSelectTurret(float volume)
    {
        source.PlayOneShot(unselectTurret, volume);
    }
    public void PlayDamageNexus(float volume)
    {
        source.PlayOneShot(nexusDamagedSound, volume);
    }
    public void PlayBuildMode(float volume)
    {
        source.PlayOneShot(buildModeOpenSound, volume);
    }
}
