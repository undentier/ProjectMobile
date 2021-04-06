using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanel : MonoBehaviour
{
    public static WavePanel instance;
    public LevelsSO level;
    public GameObject panel;

    public Transform[] slots;
    public GameObject[] buttons;
    public List<GameObject> usedButtons = new List<GameObject>();
    private List<GameObject> cacheButtons = new List<GameObject>();
    public GameObject basicTurret;
    public GameObject startWaveButton;
    public bool isBuildMode;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        panel.SetActive(false);
        for (int i = 0; i < level.blockChoice.Length; i++)
        {
            switch (level.blockChoice[i].block)
            {
                case LevelsSO.blockList.FIRERATE:
                    usedButtons.Add(buttons[0]);
                    break;
                case LevelsSO.blockList.DAMAGE:
                    usedButtons.Add(buttons[1]);
                    break;
                case LevelsSO.blockList.RANGE:
                    usedButtons.Add(buttons[2]);
                    break;
                case LevelsSO.blockList.POISON:
                    usedButtons.Add(buttons[3]);
                    break;
                case LevelsSO.blockList.SLOW:
                    usedButtons.Add(buttons[4]);
                    break;
                case LevelsSO.blockList.EXPLOSION:
                    usedButtons.Add(buttons[5]);
                    break;
                case LevelsSO.blockList.LASER:
                    usedButtons.Add(buttons[6]);
                    break;
            }
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        ActiveBuildMode();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayPanel();
        }
    }
    void DisplayPanel()
    {
        panel.SetActive(true);
        basicTurret.SetActive(true);
        startWaveButton.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            if (usedButtons.Count > 0)
            {
                int random = Random.Range(0, usedButtons.Count);
                usedButtons[random].SetActive(true);
                usedButtons[random].transform.position = slots[i].position;
                cacheButtons.Add(usedButtons[random]);
                usedButtons.Remove(usedButtons[random]);
            }
        }
        for (int i = 0; i < cacheButtons.Count; i++)
        {
            usedButtons.Add(cacheButtons[i]);
            cacheButtons.Remove(cacheButtons[i]);
        }
    }
    public void ActiveBuildMode()
    {
        DisplayPanel();
        isBuildMode = true;
        LifeManager.lifeInstance.ChangeToken(1);
    }
    public void DisableBuildMode()
    {
        isBuildMode = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        panel.SetActive(false);
        basicTurret.SetActive(false);
    }
    public void StartWaveButton()
    {
        DisableBuildMode();
        WaveSpawner.instance.StartWave();
        startWaveButton.SetActive(false);
    }
}
