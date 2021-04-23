using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

[CreateAssetMenu(fileName = "new level", menuName = "Level")]
public class LevelsSO : ScriptableObject
{
    public string levelSceneName;
    public int levelNumber;
    public int chapterNumber;
    public new string name;
    public string description;
    public Sprite blocks;
    public int score;
    public enum blockList
    {
        FIRERATE, DAMAGE, RANGE, POISON, SLOW, EXPLOSION, LASER
    }

    [Serializable]
    public struct selectedBlock
    {
        public blockList block;
        public float weight;
    }

    public selectedBlock[] blockChoice;

    public selectedBlock[] blockSprites;
    /*public bool fireRate;
    public bool damage;
    public bool range;
    public bool poison;
    public bool slow;
    public bool explosion;
    public bool laser;*/
}
