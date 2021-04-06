using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new wave", menuName = "Wave")]
public class WaveSO : ScriptableObject
{
    public enum EnemyEnum {small, medium, big};

    [Serializable]
    public struct Enemy
    {
        public EnemyEnum wichEnemy;
        public float timeBeforeNextSpawn;
        [Space]
        public int number;
        public float timeBtwSpawn;

        public Enemy(int _number = 1)
        {
            wichEnemy = 0;
            timeBeforeNextSpawn = 0f;

            number = _number;
            timeBtwSpawn = 0f;
        }
    }

    [Serializable]
    public struct Wave
    {
        public Enemy[] enemies;
    }

    [Header ("Macro time")]
    public float timeBeforeStartWave;
    public float timeBeforeEndWave;

    [Header ("Wave list")]
    public Wave[] waves;
}
