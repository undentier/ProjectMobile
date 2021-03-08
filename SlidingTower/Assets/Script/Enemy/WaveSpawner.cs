using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header ("Stats")]
    public float timeBfStart;
    public float timeBtwWave;

    [Header ("Unity Setup")]
    public Transform enemyPrefab;
    public Text uiCounter;

    public List<Transform> enemyList = new List<Transform>();

    [HideInInspector]
    public float timeCounter;
    private int waveIndex;

    private Transform spawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        spawnPoint = SpawnPoint.startpoint;
        timeCounter = timeBfStart;
    }

    private void Update()
    {
        enemyList.RemoveAll(list_item => list_item == null);

        if (timeCounter <= 0f)
        {
            timeCounter = timeBtwWave;
            StartCoroutine(SpawnWave());
        }

        timeCounter -= Time.deltaTime;
        uiCounter.text = Mathf.Round(timeCounter).ToString();
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
        Transform enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        enemyList.Add(enemy);
    }

    public static float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }

    private float SortFonction(Transform a, Transform b)
    {
        float distA = a.gameObject.GetComponent<Enemy>().distFromNexus;
        float distB = b.gameObject.GetComponent<Enemy>().distFromNexus;

        return distA.CompareTo(distB);
    }

}
