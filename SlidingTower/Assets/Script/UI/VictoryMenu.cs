using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
    [Header ("Result start")]
    public int[] scoreLvl;

    [Header ("Unity setup")]
    public Text killCounter;
    public Text hpLostCounter;
    public Image[] stars;
    public Sprite fillStar;
    private int finalScore;

    void Start()
    {
        killCounter.text = LifeManager.lifeInstance.numOfKill.ToString();
        hpLostCounter.text = (LifeManager.lifeInstance.startLife - LifeManager.lifeInstance.life).ToString();

        ResultCalculating();
    }

    void ResultCalculating()
    {
        if (LifeManager.lifeInstance.life < scoreLvl[0])
        {
            finalScore = 1;
        }
        else if (LifeManager.lifeInstance.life > scoreLvl[0] && LifeManager.lifeInstance.life < scoreLvl[1])
        {
            stars[1].sprite = fillStar;
            finalScore = 2;
        }
        
        else if (LifeManager.lifeInstance.life > scoreLvl[1])
        {
            stars[1].sprite = fillStar;
            stars[2].sprite = fillStar;
            finalScore = 3;
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.SaveScore(finalScore);
            GameManager.instance.SetScore();
        }
    }
}
