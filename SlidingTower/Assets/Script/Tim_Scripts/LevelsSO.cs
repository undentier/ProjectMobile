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
    public new string name;
    public string description;
    public Sprite blocks;
    public Sprite scoreSpriteZero;
    public Sprite scoreSpriteOne;
    public Sprite scoreSpriteTwo;
    public Sprite scoreSpriteThree;
    public int score;
    public enum blockList
    {
        FIRERATE, DAMAGE, RANGE, POISON, SLOW, EXPLOSION, LASER
    }

    [Serializable]
    public struct selectedBlock
    {
        public blockList block;
    }

    public selectedBlock[] blockChoice;

    /*public bool fireRate;
    public bool damage;
    public bool range;
    public bool poison;
    public bool slow;
    public bool explosion;
    public bool laser;*/
}
