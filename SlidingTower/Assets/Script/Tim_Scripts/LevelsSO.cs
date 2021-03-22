using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new level", menuName = "Level")]
public class LevelsSO : ScriptableObject
{
    public int levelNumber;
    public new string name;
    public string description;
    public Sprite blocks;
    public Sprite scoreSpriteZero;
    public Sprite scoreSpriteOne;
    public Sprite scoreSpriteTwo;
    public Sprite scoreSpriteThree;
    public int score;
}
