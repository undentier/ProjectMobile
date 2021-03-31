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
    }

    [Serializable]
    public struct Wave
    {
        public Enemy[] enemies;
    }

    public float timeBeforeStartWave;
    public float timeBeforeEndWave;
    public Wave[] waves;
}
