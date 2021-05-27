using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip enemyDeath;
    public AudioClip enemyHurt;
    public AudioClip[] enemySpawnSounds;

    public static EnemySoundManager I;
    void Awake()
    {
        I = this;
    }

    public void Death(float volume)
    {
        source.PlayOneShot(enemyDeath, volume);
    }
    public void Hurt(float volume)
    {
        source.PlayOneShot(enemyHurt, volume);
    }

    public void PlaySpawnSound(int enemySize, float volume)
    {
        source.PlayOneShot(enemySpawnSounds[enemySize], volume);
    }
}
