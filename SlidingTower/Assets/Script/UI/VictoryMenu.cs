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

    void Start()
    {
        killCounter.text = LifeManager.lifeInstance.numOfKill.ToString();
        hpLostCounter.text = (LifeManager.lifeInstance.startLife - LifeManager.lifeInstance.life).ToString();

        ResultCalculating();
    }

    void ResultCalculating()
    {
        if (LifeManager.lifeInstance.life > scoreLvl[0])
        {
            stars[1].sprite = fillStar;
        }
        
        if (LifeManager.lifeInstance.life > scoreLvl[1])
        {
            stars[2].sprite = fillStar;
        }
    }
}
