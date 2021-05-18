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

    public List<Image> blockImages;
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
        GameManager.instance.GetLevelInfo(level);
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

        for (int i = 0; i < level.blockChoice.Length; i++)
        {
            switch (level.blockChoice[i].block)
            {
                case LevelsSO.blockList.FIRERATE:
                    blockImages[i].sprite = speedBlocSprite;
                    break;
                case LevelsSO.blockList.DAMAGE:
                    blockImages[i].sprite = damageBlocSprite;
                    break;
                case LevelsSO.blockList.RANGE:
                    // mettre ici l'icone de range
                    break;
                case LevelsSO.blockList.POISON:
                    blockImages[i].sprite = poisonBlocSprite;
                    break;
                case LevelsSO.blockList.SLOW:
                    blockImages[i].sprite = slowBlocSprite;
                    break;
                case LevelsSO.blockList.EXPLOSION:
                    blockImages[i].sprite = explosionBlocSprite;
                    break;
                case LevelsSO.blockList.LASER:
                    blockImages[i].sprite = laserBlocSprite;
                    break;
                default:
                    blockImages[i].enabled = false;
                        break;
            }
        }
        for (int i = level.blockChoice.Length; i < blockImages.Count; i++)
        {
            blockImages[i].enabled = false;
        }
        levelNumber = level.levelNumber;
        levelNumberText.text = levelNumber.ToString();
        UpdateScoreDisplay();
    }
}
