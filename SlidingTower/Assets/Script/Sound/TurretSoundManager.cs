using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip placeSound;
    public AudioClip slideSound;
    public AudioClip turretGenericPlaceSound;
    public AudioClip[] turretPlaceSound;
    public AudioClip shootSound;
    public AudioClip explosionSound;
    public AudioClip previSlideSound;

    public static TurretSoundManager I;
    void Awake()
    {
        I = this;
    }

    public void Place(float volume)
    {
        source.PlayOneShot(placeSound, volume);
    }
    public void Slide(float volume)
    {
        source.PlayOneShot(slideSound, volume);
    }
    public void Shoot(float volume)
    {
        source.PlayOneShot(shootSound, volume);
    }
    public void PlayExplodeSound(float volume)
    {
        source.PlayOneShot(explosionSound, volume);
    }

    public void EffectSound(int turretType, float volume)
    {
        if(turretType > 0)
            source.PlayOneShot(turretPlaceSound[turretType - 1], volume);
    }

    public void PlaceTurret(float volume)
    {
        source.PlayOneShot(turretGenericPlaceSound, 1);
    }
    public void PlayPreviSlide(float volume)
    {
        source.PlayOneShot(previSlideSound, volume);
    }
}
