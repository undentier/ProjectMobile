using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public LevelsSO level;

    public Text nameText;
    public Text descriptionText;

    public Image thumbnailImage;
    public Image scoreImage;

    public Text levelNumberText;
    public int levelNumber;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }
    public void ScoreTracker()
    {
        level.score = Random.Range(0, 4);
        SpriteDisplay();
        score = level.score;

    }
    public void SpriteDisplay()
    {
        if (level.score == 0)
        {
            scoreImage.sprite = level.scoreSpriteZero;
        }
        else if (level.score == 1)
        {
            scoreImage.sprite = level.scoreSpriteOne;
        }
        else if (level.score == 2)
        {
            scoreImage.sprite = level.scoreSpriteTwo;
        }
        else if (level.score == 3)
        {
            scoreImage.sprite = level.scoreSpriteThree;
        }
    }
    public void UpdateDisplay()
    {
        nameText.text = level.name;
        descriptionText.text = level.description;

        thumbnailImage.sprite = level.blocks;
        scoreImage.sprite = level.scoreSpriteZero;

        levelNumber = level.levelNumber;
        levelNumberText.text = levelNumber.ToString();
        Debug.Log(level.name);
        SpriteDisplay();
        score = level.score;
    }
}
