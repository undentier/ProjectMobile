using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartEnemyPreview : MonoBehaviour
{
    [Header("Low enemy")]
    public GameObject lowGeneral;
    public TextMeshProUGUI lowCounter;

    [Header("Mid enemy")]
    public GameObject midGeneral;
    public TextMeshProUGUI midCounter;

    [Header("Big enemy")]
    public GameObject bigGeneral;
    public TextMeshProUGUI bigCounter;

    [Header("Global setup")]
    public GameObject generalEnemyPreview;

    private bool uiActive;

    private void Start()
    {
        generalEnemyPreview.SetActive(false);
        ActualiseEnemy();
    }

    private void Update()
    {
        if (WavePanel.instance.isBuildMode == false)
        {
            generalEnemyPreview.SetActive(false);
        }
    }

    public void ActualiseEnemy()
    {
        
        if (WaveSpawner.instance.nextTotalLowEnemyNumber > 0)
        {
            lowGeneral.SetActive(true);
            lowCounter.text = WaveSpawner.instance.nextTotalLowEnemyNumber.ToString();
        }
        else
        {
            lowGeneral.SetActive(false);
        }

        if (WaveSpawner.instance.nextTotalMidEnemyNumber > 0)
        {
            midGeneral.SetActive(true);
            midCounter.text = WaveSpawner.instance.nextTotalMidEnemyNumber.ToString();
        }
        else
        {
            midGeneral.SetActive(false);
        }

        if (WaveSpawner.instance.nextTotalBigEnemyNumber > 0)
        {
            bigGeneral.SetActive(true);
            bigCounter.text = WaveSpawner.instance.nextTotalBigEnemyNumber.ToString();
        }
        else
        {
            bigGeneral.SetActive(false);
        }
        
    }

    public void GetingTouch()
    {
        if (WavePanel.instance.isBuildMode == true)
        {
            if (uiActive)
            {
                uiActive = false;
                generalEnemyPreview.SetActive(false);
            }
            else
            {
                uiActive = true;
                generalEnemyPreview.SetActive(true);
                ActualiseEnemy();
            }
        }
    }

}