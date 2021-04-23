using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanel : MonoBehaviour
{
    #region variable
    public static WavePanel instance;

    public LevelsSO level;
    public GameObject panel;

    public bool[] canBuildTurretWaves;
    [HideInInspector]
    public int canBuildTurretIndex;
    public Transform[] slots;
    public GameObject[] buttons;
    private List<GameObject> usedButtons = new List<GameObject>();
    private List<GameObject> cacheButtons = new List<GameObject>();
    [Space]
    public GameObject basicTurret;
    public GameObject startWaveButton;
    public bool isBuildMode;
    public bool isFirstWave = true;
    public Animator anim;
    #endregion

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //panel.SetActive(false);
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

    public void DisplayPanel()
    {
        anim.SetInteger("state", 1);
        LifeManager.lifeInstance.ChangeToken(1);

        if (canBuildTurretWaves[canBuildTurretIndex] == true)
        {
            basicTurret.SetActive(true);
        }

        if(isFirstWave == true)
        {
            isFirstWave = false;
        }
        else
        {
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
            for (int i = 0; i < 2; i++)
            {
                usedButtons.Add(cacheButtons[0]);
                cacheButtons.Remove(cacheButtons[0]);
            }
        }
    }

    public void ActiveBuildMode()
    {
        isBuildMode = true;

        if (isFirstWave && canBuildTurretWaves[0] == false)
        {
            isFirstWave = false;
            return;
        }
        else
        {
            DisplayPanel();
        }
        canBuildTurretIndex++;
    }
    public void DisableBuildMode()
    {
        anim.SetInteger("state", 2);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        //panel.SetActive(false);
        basicTurret.SetActive(false);
    }
    public void StartWaveButton()
    {
        isBuildMode = false;
        DisableBuildMode();
        WaveSpawner.instance.StartWave();
        startWaveButton.SetActive(false);
    }
}
