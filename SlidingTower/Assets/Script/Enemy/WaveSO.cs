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
        public int number;
        public EnemyEnum wichEnemy;
        public float timeBeforeNextSpawn;
    }

    [Serializable]
    public struct Wave
    {
        public Enemy[] enemies;
    }

    public Wave[] waves;
}
