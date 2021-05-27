﻿using System.Collections;
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

    public GameObject blockContainer;

    public Text levelNumberText;
    public int levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }
    public void ScoreTracker()
    {
        UISoundsManager.uiSoundsManager.PlayStartSound(1);
        GameManager.instance.GetLevelInfo(level);

        StartCoroutine(WaitFadeEnd());
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
                case LevelsSO.BlockList.FIRERATE:
                    blockImages[i].sprite = speedBlocSprite;
                    break;
                case LevelsSO.BlockList.DAMAGE:
                    blockImages[i].sprite = damageBlocSprite;
                    break;
                case LevelsSO.BlockList.RANGE:
                    // mettre ici l'icone de range
                    break;
                case LevelsSO.BlockList.POISON:
                    blockImages[i].sprite = poisonBlocSprite;
                    break;
                case LevelsSO.BlockList.SLOW:
                    blockImages[i].sprite = slowBlocSprite;
                    break;
                case LevelsSO.BlockList.EXPLOSION:
                    blockImages[i].sprite = explosionBlocSprite;
                    break;
                case LevelsSO.BlockList.LASER:
                    blockImages[i].sprite = laserBlocSprite;
                    break;
                default:
                    Destroy(blockImages[i].gameObject);
                        break;
            }
        }
        
        for (int i = level.blockChoice.Length; i < blockImages.Count; i++)
        {
            Destroy(blockImages[i].gameObject);
        }
        for (int i = 0; i < blockImages.Count; i++)
        {
            if (blockImages[i].sprite == null)
            {
                blockImages.RemoveAt(i);
                //blockContainer.transform.position = new Vector3(transform.position.x + 80, transform.position.y, transform.position.z);
            }
        }

        levelNumber = level.levelNumber;
        levelNumberText.text = "Level " + levelNumber.ToString();
        UpdateScoreDisplay();
    }

    IEnumerator WaitFadeEnd()
    {
        FadeManager.instance.FadeIn(FadeManager.instance.fadeImage, FadeManager.instance.fadeInTime, false);
        yield return new WaitForSeconds(FadeManager.instance.fadeInTime);
        SceneManager.LoadScene(level.levelSceneName);
    }
}
