﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = level.name;
        descriptionText.text = level.description;

        thumbnailImage.sprite = level.blocks;
        scoreImage.sprite = level.scoreSpriteZero;

        levelNumberText.text = level.levelNumber.ToString();
        Debug.Log(level.name);
    }
    public void ScoreTracker()
    {
        level.score = Random.Range(0, 4);
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
}
