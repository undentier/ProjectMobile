using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanel : MonoBehaviour
{
    #region variable
    public static WavePanel instance;

    [Header ("GD setup")]
    public LevelsSO level;
    public GameObject panel;
    public bool[] canBuildTurretWaves;

    [Header ("Unity setup")]
    public Animator buildPanelAnim;
    public Animator startWaveButtonAnim;
    [Space]
    public GameObject basicTurret;
    public GameObject startWaveButton;
    [Space]
    public Transform[] slots;
    public GameObject[] buttons;

    private List<GameObject> usedButtons = new List<GameObject>();
    public int canBuildTurretIndex;
    public bool isBuildMode;
    [HideInInspector] public bool isFirstWave = true;
    #endregion

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DisableAllButtons();

        ActiveBuildMode();
    }

    public void DisplayPanel()
    {
        buildPanelAnim.SetInteger("state", 1);
        LifeManager.lifeInstance.ChangeToken(1);
        PlayerSoundManager.I.PlayBuildMode(1);
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
            startWaveButtonAnim.SetInteger("startState", 2);
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
        buildPanelAnim.SetInteger("state", 2);
        DisableAllButtons();
        basicTurret.SetActive(false);
    }
    public void StartWaveButton()
    {
        startWaveButtonAnim.SetInteger("startState", 1);
        PlayerSoundManager.I.StartWave(1);
        isBuildMode = false;
        DisableBuildMode();
        WaveSpawner.instance.StartWave();
    }

    void SetButtonList()
    {
        for (int i = 0; i < level.blockChoice.Length; i++)
        {
            switch (level.blockChoice[i].block)
            {
                case LevelsSO.BlockList.FIRERATE:
                    for (int fireRateIndex = 0; fireRateIndex < level.blockChoice[i].percentage; fireRateIndex++)
                    {
                        usedButtons.Add(buttons[0]);
                    }
                    break;
                case LevelsSO.BlockList.DAMAGE:
                    for (int damageIndex = 0; damageIndex < level.blockChoice[i].percentage; damageIndex++)
                    {
                        usedButtons.Add(buttons[1]);
                    }
                    break;
                case LevelsSO.BlockList.RANGE:
                    for (int rangeIndex = 0; rangeIndex < level.blockChoice[i].percentage; rangeIndex++)
                    {
                        usedButtons.Add(buttons[2]);
                    }
                    break;
                case LevelsSO.BlockList.POISON:
                    for (int poisonIndex = 0; poisonIndex < level.blockChoice[i].percentage; poisonIndex++)
                    {
                        usedButtons.Add(buttons[3]);
                    }
                    break;
                case LevelsSO.BlockList.SLOW:
                    for (int slowIndex = 0; slowIndex < level.blockChoice[i].percentage; slowIndex++)
                    {
                        usedButtons.Add(buttons[4]);
                    }
                    break;
                case LevelsSO.BlockList.EXPLOSION:
                    for (int explosionIndex = 0; explosionIndex < level.blockChoice[i].percentage; explosionIndex++)
                    {
                        usedButtons.Add(buttons[5]);
                    }
                    break;
                case LevelsSO.BlockList.LASER:
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


    public void ShowStartWaveButton()
    {
        StartCoroutine(WaitBeforeShowStartWave());
    }

    public IEnumerator WaitBeforeShowStartWave()
    {
        yield return new WaitForSeconds(1.3f);
        startWaveButtonAnim.SetInteger("startState", 2);
    }
}
