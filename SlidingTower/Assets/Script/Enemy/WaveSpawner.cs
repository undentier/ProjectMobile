using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header("Put waveSO here")]
    public WaveSO[] levelWaves;

    [Header("Info spawner")]
    public Transform[] spawnPoints;

    [Header("Unity setup")]
    public GameObject smallEnemy;
    public GameObject mediumEnemy;
    public GameObject bigEnemy;


    [Header("Macro info Wave")]
    public float timeBeforeEndWave;

    [HideInInspector]
    public int waveIndex;
    [HideInInspector]
    public bool enemyAlive;
    [HideInInspector]
    public bool waveSpawn;
    [HideInInspector]
    public List<Enemy> enemyList = new List<Enemy>();
    private bool canCheck;
    private int numOfWaveFinish;

    //[HideInInspector]
    public int nextTotalLowEnemyNumber;
    //[HideInInspector]
    public int nextTotalMidEnemyNumber;
    //[HideInInspector]
    public int nextTotalBigEnemyNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //RefreshNextWaveComposition();
        GetTotaleWaveCompo();
    }
    void Update()
    {
        enemyList.RemoveAll(list_item => list_item == null);
        CheckIfEnemyAlive();
    }

    IEnumerator SpawnWave(WaveSO levelWave, Transform spawnPoint)
    {
        yield return new WaitForSeconds(levelWave.timeBeforeStartWave);

        enemyAlive = true;

        for (int i = 0; i < levelWave.waves[waveIndex].enemies.Length; i++)
        {
            for (int t = 0; t < levelWave.waves[waveIndex].enemies[i].number; t++)
            {
                GameObject enemyToSpawn = GetEnemyToSpawn(levelWave.waves[waveIndex].enemies[i].wichEnemy);
                GameObject actualEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                enemyList.Add(actualEnemy.GetComponent<Enemy>());
                yield return new WaitForSeconds(levelWave.waves[waveIndex].enemies[i].timeBtwSpawn);
            }

            yield return new WaitForSeconds(levelWave.waves[waveIndex].enemies[i].timeBeforeNextSpawn);
        }
        numOfWaveFinish++;
    }

    IEnumerator WaitBfrEndWave()
    {
        yield return new WaitForSeconds(timeBeforeEndWave);
        waveIndex++;
        waveSpawn = false;

        //RefreshNextWaveComposition();

        VictoryDetection();
    }

    void CheckIfEnemyAlive()
    {
        if (enemyAlive && enemyList.Count == 0 && canCheck)
        {
            enemyAlive = false;
            canCheck = false;
            StartCoroutine(WaitBfrEndWave());
        }
        else if (!canCheck && numOfWaveFinish == levelWaves.Length)
        {
            canCheck = true;
            numOfWaveFinish = 0;
        }
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

    public void StartWave()
    {
        if (!waveSpawn && waveIndex < levelWaves[0].waves.Length)
        {
            waveSpawn = true;

            for (int i = 0; i < levelWaves.Length; i++)
            {
                StartCoroutine(SpawnWave(levelWaves[i], spawnPoints[i]));
            }
        }
    }

    void VictoryDetection()
    {
        if (waveIndex >= levelWaves[0].waves.Length && !enemyAlive)
        {
            UIManager.instance.DisplayVictoryMenu();
        }
        else
        {
            UIManager.instance.previewScript.ActualiseEnemy();
        }
    }

        int lowEnemyNumber;
        int midEnemyNumber;
        int bigEnemyNumber;

    void GetNextWaveComposition(WaveSO wichWave, int nextLowEnemyNumber, int nextMidEnemyNumber, int nextBigEnemyNumber)
    {
        lowEnemyNumber = 0;
        midEnemyNumber = 0;
        bigEnemyNumber = 0;

        if (waveIndex < wichWave.waves.Length)
        {
            for (int i = 0; i < wichWave.waves[waveIndex].enemies.Length; i++)
            {
                switch (wichWave.waves[waveIndex].enemies[i].wichEnemy)
                {
                    case WaveSO.EnemyEnum.small:
                        lowEnemyNumber += wichWave.waves[waveIndex].enemies[i].number;
                        break;
                    case WaveSO.EnemyEnum.medium:
                        midEnemyNumber += wichWave.waves[waveIndex].enemies[i].number;
                        break;
                    case WaveSO.EnemyEnum.big:
                        bigEnemyNumber += wichWave.waves[waveIndex].enemies[i].number;
                        break;
                }
            }

        }
     
            nextLowEnemyNumber += lowEnemyNumber;
            nextMidEnemyNumber += midEnemyNumber;
            nextBigEnemyNumber += bigEnemyNumber;
    }

    void GetTotaleWaveCompo()
    {
        for (int i = 0; i < levelWaves.Length; i++)
        {
            Debug.Log("get total wave");
            GetNextWaveComposition(levelWaves[i], nextTotalLowEnemyNumber, nextTotalMidEnemyNumber, nextTotalBigEnemyNumber);
        }
    }
}
