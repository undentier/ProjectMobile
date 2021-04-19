using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    public LevelsSO level;

    public Text nameText;
    public Text descriptionText;

    public Image thumbnailImage;
    public Image scoreImage;
    public Sprite zeroStarSprite;
    public Sprite oneStarSprite;
    public Sprite twoStarSprite;
    public Sprite threeStarSprite;

    public Sprite damageBlocSprite;
    public Sprite explosionBlocSprite;
    public Sprite speedBlocSprite;
    public Sprite laserBlocSprite;
    public Sprite poisonBlocSprite;
    public Sprite slowBlocSprite;

    public Text levelNumberText;
    public int levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }
    public void ScoreTracker()
    {
        /*level.score = Random.Range(0, 4);
        UpdateScoreDisplay();*/
        SceneManager.LoadScene(level.levelSceneName);

    }
    public void UpdateScoreDisplay()
    {
        if (level.score == 0)
        {
            scoreImage.sprite = zeroStarSprite;
        }
        else if (level.score == 1)
        {
            scoreImage.sprite = oneStarSprite;
        }
        else if (level.score == 2)
        {
            scoreImage.sprite = twoStarSprite;
        }
        else if (level.score == 3)
        {
            scoreImage.sprite = threeStarSprite;
        }
    }
    public void UpdateDisplay()
    {
        nameText.text = level.name;
        descriptionText.text = level.description;

        thumbnailImage.sprite = level.blocks;

        levelNumber = level.levelNumber;
        levelNumberText.text = levelNumber.ToString();
        Debug.Log(level.name);
        UpdateScoreDisplay();
    }
}
