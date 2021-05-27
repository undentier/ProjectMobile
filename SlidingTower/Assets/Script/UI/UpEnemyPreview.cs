using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpEnemyPreview : MonoBehaviour
{
    public static UpEnemyPreview instance;

    [Header("Low enemy")]
    //public GameObject lowGeneral;
    public TextMeshProUGUI lowCounter;

    [Header("Mid enemy")]
    //public GameObject midGeneral;
    public TextMeshProUGUI midCounter;

    [Header("Big enemy")]
    //public GameObject bigGeneral;
    public TextMeshProUGUI bigCounter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple UpEnnemyPreview");
        }
    }

    void Start()
    {
        ActualyseEnemyCounter();
    }

    public void ActualyseEnemyCounter()
    {
        lowCounter.text = WaveSpawner.instance.nextTotalLowEnemyNumber.ToString();

        midCounter.text = WaveSpawner.instance.nextTotalMidEnemyNumber.ToString();

        bigCounter.text = WaveSpawner.instance.nextTotalBigEnemyNumber.ToString();
    }
}
