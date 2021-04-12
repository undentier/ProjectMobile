using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header("Put waveSO here")]
    public WaveSO levelWaves;

    [Header("Unity setup")]
    public GameObject smallEnemy;
    public GameObject mediumEnemy;
    public GameObject bigEnemy;

    [Header("UI")]
    public bool activateUI;
    public Text counterActualWaveCounter;
    public Text counterTotalWave;
    public GameObject counterNextWave;

    [HideInInspector]
    public int waveIndex;
    [HideInInspector]
    public bool enemyAlive;
    [HideInInspector]
    public bool waveSpawn;
    [HideInInspector]
    public List<Enemy> enemyList = new List<Enemy>();
    private Transform spawnPoint;
    private bool canCheck;

    [HideInInspector]
    public int nextLowEnemyNumber;
    [HideInInspector]
    public int nextMidEnemyNumber;
    [HideInInspector]
    public int nextBigEnemyNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        spawnPoint = StartInfo.startPoint;
    }
    void Update()
    {
        enemyList.RemoveAll(list_item => list_item == null);
        CheckIfEnemyAlive();
        UiSysteme();
    }

    IEnumerator SpawnWave()
    {
        waveSpawn = true;
        yield return new WaitForSeconds(levelWaves.timeBeforeStartWave);

        enemyAlive = true;

        for (int i = 0; i < levelWaves.waves[waveIndex].enemies.Length; i++)
        {
            for (int t = 0; t < levelWaves.waves[waveIndex].enemies[i].number; t++)
            {
                GameObject enemyToSpawn = GetEnemyToSpawn(levelWaves.waves[waveIndex].enemies[i].wichEnemy);
                GameObject actualEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                enemyList.Add(actualEnemy.GetComponent<Enemy>());
                yield return new WaitForSeconds(levelWaves.waves[waveIndex].enemies[i].timeBtwSpawn);
            }

            yield return new WaitForSeconds(levelWaves.waves[waveIndex].enemies[i].timeBeforeNextSpawn);
        }
        canCheck = true;
    }

    IEnumerator WaitBfrEndWave()
    {
        yield return new WaitForSeconds(levelWaves.timeBeforeEndWave);
        waveIndex++;
        waveSpawn = false;

        RefreshNextWaveComposition();

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

    void UiSysteme()
    {
        if (activateUI)
        {
            counterActualWaveCounter.text = waveIndex.ToString();
            counterTotalWave.text = levelWaves.waves.Length.ToString();
        }
    }
    public void StartWave()
    {
        if (!waveSpawn && waveIndex < levelWaves.waves.Length)
        {
            StartCoroutine(SpawnWave());
        }
    }

    void VictoryDetection()
    {
        if (waveIndex >= levelWaves.waves.Length && !enemyAlive)
        {
            UIManager.instance.DisplayVictoryMenu();
        }
        else
        {
            UIManager.instance.previewScript.ActualiseEnemy();
        }
    }

    void RefreshNextWaveComposition()
    {
        if (waveIndex < levelWaves.waves.Length)
        {
            nextLowEnemyNumber = 0;
            nextMidEnemyNumber = 0;
            nextBigEnemyNumber = 0;

            for (int i = 0; i < levelWaves.waves[waveIndex].enemies.Length; i++)
            {
                switch (levelWaves.waves[waveIndex].enemies[i].wichEnemy)
                {
                    case WaveSO.EnemyEnum.small:
                        nextLowEnemyNumber += levelWaves.waves[waveIndex].enemies[i].number;
                        break;
                    case WaveSO.EnemyEnum.medium:
                        nextMidEnemyNumber += levelWaves.waves[waveIndex].enemies[i].number;
                        break;
                    case WaveSO.EnemyEnum.big:
                        nextBigEnemyNumber += levelWaves.waves[waveIndex].enemies[i].number;
                        break;
                }
            }
        }
    }
}
