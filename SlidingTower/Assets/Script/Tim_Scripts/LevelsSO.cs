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
    public enum BlockList
    {
        FIRERATE, DAMAGE, RANGE, POISON, SLOW, EXPLOSION, LASER, NONE
    }

    [Serializable]
    public struct SelectedBlock
    {
        public BlockList block;
        public float percentage;
    }

    public SelectedBlock[] blockChoice;
}
