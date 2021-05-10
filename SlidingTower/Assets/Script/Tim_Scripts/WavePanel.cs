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
    [Space]
    public GameObject basicTurret;
    public GameObject startWaveButton;
    public bool isBuildMode;
    public bool isFirstWave = true;
    private Animator anim;
    #endregion

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();

        DisableAllButtons();

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
            SetButtonList();
            ChooseButtons();
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
        DisableAllButtons();
        basicTurret.SetActive(false);
    }
    public void StartWaveButton()
    {
        isBuildMode = false;
        DisableBuildMode();
        WaveSpawner.instance.StartWave();
        startWaveButton.SetActive(false);
    }

    void SetButtonList()
    {
        for (int i = 0; i < level.blockChoice.Length; i++)
        {
            switch (level.blockChoice[i].block)
            {
                case LevelsSO.blockList.FIRERATE:
                    for (int fireRateIndex = 0; fireRateIndex < level.blockChoice[i].percentage; fireRateIndex++)
                    {
                        usedButtons.Add(buttons[0]);
                    }
                    break;
                case LevelsSO.blockList.DAMAGE:
                    for (int damageIndex = 0; damageIndex < level.blockChoice[i].percentage; damageIndex++)
                    {
                        usedButtons.Add(buttons[1]);
                    }
                    break;
                case LevelsSO.blockList.RANGE:
                    for (int rangeIndex = 0; rangeIndex < level.blockChoice[i].percentage; rangeIndex++)
                    {
                        usedButtons.Add(buttons[2]);
                    }
                    break;
                case LevelsSO.blockList.POISON:
                    for (int poisonIndex = 0; poisonIndex < level.blockChoice[i].percentage; poisonIndex++)
                    {
                        usedButtons.Add(buttons[3]);
                    }
                    break;
                case LevelsSO.blockList.SLOW:
                    for (int slowIndex = 0; slowIndex < level.blockChoice[i].percentage; slowIndex++)
                    {
                        usedButtons.Add(buttons[4]);
                    }
                    break;
                case LevelsSO.blockList.EXPLOSION:
                    for (int explosionIndex = 0; explosionIndex < level.blockChoice[i].percentage; explosionIndex++)
                    {
                        usedButtons.Add(buttons[5]);
                    }
                    break;
                case LevelsSO.blockList.LASER:
                    for (int explosionIndex = 0; explosionIndex < level.blockChoice[i].percentage; explosionIndex++)
                    {
                        usedButtons.Add(buttons[6]);
                    }
                    break;
            }
        }
    }
    void DisableAllButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    void ChooseButtons()
    {
        for (int i = 0; i < 2; i++)
        {
            if (usedButtons.Count > 0)
            {
                int random = Random.Range(0, usedButtons.Count);
                wichButton(usedButtons[random]).SetActive(true);
                wichButton(usedButtons[random]).transform.position = slots[i].position;

                usedButtons.RemoveAll(list_item => list_item == wichButton(usedButtons[random]));
            }
        }
    }

    public GameObject wichButton(GameObject button)
    {
        if (button == buttons[0])
        {
            return buttons[0];
        }
        else if (button == buttons[1])
        {
            return buttons[1];
        }
        else if (button == buttons[2])
        {
            return buttons[2];
        }
        else if (button == buttons[3])
        {
            return buttons[3];
        }
        else if (button == buttons[4])
        {
            return buttons[4];
        }
        else if (button == buttons[5])
        {
            return buttons[5];
        }
        else if (button == buttons[6])
        {
            return buttons[6];
        }
        else
        {
            return null;
        }
    }
}
