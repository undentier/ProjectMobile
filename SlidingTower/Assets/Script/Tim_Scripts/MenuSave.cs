using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSave : MonoBehaviour
{
    public static MenuSave instance;
    public LevelManager levelmanager;
    public GameObject playMenu;
    public GameObject retourChapters;
    public GameObject retourMenu;
    public GameObject verticalPanel;
    public GameObject horizontalPanel;
    public GameObject titleMenu;
    public GameObject gaStuff;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Load();
        }
    }
    private void Start()
    {
        instance = this;
        Load();
    }

    public void Load()
    {
        if (MenuCheck.index != 0)
        {
            playMenu.SetActive(true);
            retourMenu.SetActive(false);
            retourChapters.SetActive(true);
            verticalPanel.SetActive(false);
            horizontalPanel.SetActive(true);
            gaStuff.SetActive(false);
            if (MenuCheck.index == 1)
            {
                levelmanager.InitializeSelectionPanel(0);
            }
            else if (MenuCheck.index == 2)
            {
                levelmanager.InitializeSelectionPanel(1);
            }
            else if (MenuCheck.index == 3)
            {
                levelmanager.InitializeSelectionPanel(2);
            }
            titleMenu.SetActive(false);
        }
    }
}
