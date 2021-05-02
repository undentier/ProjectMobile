using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPreview : MonoBehaviour
{
    [Header ("Low enemy")]
    public GameObject lowGeneral;
    public Text lowCounter;

    [Header("Mid enemy")]
    public GameObject midGeneral;
    public Text midCounter;

    [Header("Big enemy")]
    public GameObject bigGeneral;
    public Text bigCounter;

    [Header("Global setup")]
    public GameObject generalEnemyPreview;

    private void Start()
    {
        generalEnemyPreview.SetActive(false);
    }

    public void ActualiseEnemy()
    {
        /*
        generalEnemyPreview.SetActive(true);

        if (WaveSpawner.instance.nextLowEnemyNumber > 0)
        {
            lowGeneral.SetActive(true);
            lowCounter.text = WaveSpawner.instance.nextLowEnemyNumber.ToString();
        }
        else
        {
            lowGeneral.SetActive(false);
        }

        if (WaveSpawner.instance.nextMidEnemyNumber > 0)
        {
            midGeneral.SetActive(true);
            midCounter.text = WaveSpawner.instance.nextMidEnemyNumber.ToString();
        }
        else
        {
            midGeneral.SetActive(false);
        }

        if (WaveSpawner.instance.nextBigEnemyNumber > 0)
        {
            bigGeneral.SetActive(true);
            bigCounter.text = WaveSpawner.instance.nextBigEnemyNumber.ToString();
        }
        else
        {
            bigGeneral.SetActive(false);
        }
        */
    }

    public void ContinueButton()
    {
        generalEnemyPreview.SetActive(false);

        lowGeneral.SetActive(false);
        midGeneral.SetActive(false);
        bigGeneral.SetActive(false);

        WavePanel.instance.ActiveBuildMode();
    }

}
