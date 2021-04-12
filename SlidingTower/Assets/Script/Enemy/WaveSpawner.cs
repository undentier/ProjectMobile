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
    public Text counterBfrWaveSpawn;
    public Text counterActualWaveCounter;
    public Text counterTotalWave;
    public GameObject counterNextWave;

    [HideInInspector]
    public int waveIndex;
    [HideInInspector]
    public bool enemyAlive;
    [HideInInspector]
    public bool waveSpawn;
    //[HideInInspector]
    public List<Enemy> enemyList = new List<Enemy>();
    private float timeCounter;
    private Transform spawnPoint;
    private bool canCheck;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (activateUI)
        {
            counterNextWave.SetActive(false);
        }
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
        if (activateUI)
        {
            counterNextWave.SetActive(true);
        }
        yield return new WaitForSeconds(levelWaves.timeBeforeStartWave);

        if (activateUI)
        {
            counterNextWave.SetActive(false);
        }

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

        WavePanel.instance.ActiveBuildMode();
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
            if (timeCounter >= 0)
            {
                timeCounter -= Time.deltaTime;
            }
            else
            {
                timeCounter = 0f;
            }
            counterBfrWaveSpawn.text = Mathf.Round(timeCounter).ToString();
            counterActualWaveCounter.text = waveIndex.ToString();
            counterTotalWave.text = levelWaves.waves.Length.ToString();
        }
    }
    public void StartWave()
    {
        if (!waveSpawn)
        {
            StartCoroutine(SpawnWave());
            timeCounter = levelWaves.timeBeforeStartWave;
        }
    }
}
