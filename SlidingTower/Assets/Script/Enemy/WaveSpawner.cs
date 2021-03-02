﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    [Header ("Stats")]
    public float timeBfStart;
    public float timeBtwWave;

    [Header ("Unity Setup")]
    public Transform enemyPrefab;
    public Text uiCounter;

    public static List<Transform> enemyList = new List<Transform>();

    [HideInInspector]
    public float timeCounter;
    private int waveIndex;

    private Transform spawnPoint;

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

    
}
