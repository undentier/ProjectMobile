using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayerInfo : MonoBehaviour
{
    [Header("Life")]
    public Text healthCounter;

    [Header("Wave counter")]
    public Text actualWaveCounter;
    public Text totalWaveCounter;

    void Update()
    {
        healthCounter.text = LifeManager.lifeInstance.life.ToString();

        actualWaveCounter.text = WaveSpawner.instance.waveIndex.ToString();
        totalWaveCounter.text = WaveSpawner.instance.levelWaves[0].waves.Length.ToString();
    }
}
