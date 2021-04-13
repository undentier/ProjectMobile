﻿using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager lifeInstance;

    [Header("Stats")]
    public int life;
    public int buildToken;

    [Header("Unity Setup")]
    public Text lifeCounter;

    [HideInInspector]
    public int numOfKill;
    [HideInInspector]
    public int startLife;

    private void Awake()
    {
        if (lifeInstance != null)
        {
            Debug.Log("Double manager attention");
            return;
        }
        else
        {
            lifeInstance = this;
        }
    }

    private void Start()
    {
        startLife = life;
    }

    private void Update()
    {
        lifeCounter.text = life.ToString();
    }

    public void DamagePlayer(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            UIManager.instance.gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ChangeToken(int tokenNumber)
    {
        buildToken += tokenNumber;
        if (buildToken <= 0)
        {
            WavePanel.instance.DisableBuildMode();
        }
    }

    public void AddKillScore(int score)
    {
        numOfKill += score;
    }
}
