using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawerTest : MonoBehaviour
{
    public float timeBeforeStartWave;
    public int waveIndex;
    public bool waveAlive;

    [Header ("Unity setup")]
    public GameObject smallEnemy;
    public GameObject mediumEnemy;
    public GameObject bigEnemy;
    public WaveSO levelWaves;
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !waveAlive)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        waveAlive = true;
        yield return new WaitForSeconds(timeBeforeStartWave);

        for (int i = 0; i < levelWaves.waves[waveIndex].enemies.Length; i++)
        {
            GameObject enemyToSpawn = GetEnemyToSpawn(levelWaves.waves[waveIndex].enemies[i].wichEnemy);
            Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(levelWaves.waves[waveIndex].enemies[i].timeBeforeNextSpawn);
        }
        waveAlive = false;
        waveIndex++;
    }

    
    private GameObject GetEnemyToSpawn(WaveSO.EnemyEnum wichEnemy)
    {
        switch (wichEnemy)
        {
            case WaveSO.EnemyEnum.small:
                return smallEnemy;
            case WaveSO.EnemyEnum.medium:
                return mediumEnemy;
            case WaveSO.EnemyEnum.big:
                return bigEnemy;
        }
        return null;
    }
}
