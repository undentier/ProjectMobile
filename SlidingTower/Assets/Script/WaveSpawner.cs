using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBfStart;
    public float timeBtwWave;

    [HideInInspector]
    public float timeCounter;

    private int waveIndex;


    private void Start()
    {
        timeCounter = timeBfStart;
    }

    private void Update()
    {
        if (timeCounter <= 0f)
        {
            timeCounter = timeBtwWave;
            StartCoroutine(SpawnWave());
        }

        timeCounter -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
