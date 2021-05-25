using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSave : MonoBehaviour
{
    public static MenuSave instance;
    private MenuCheck menucheck;
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
        menucheck = GameManager.instance.GetComponent<MenuCheck>();
        Load();
    }

    public void Load()
    {
        if (menucheck.index != 0)
        {
            playMenu.SetActive(true);
            retourMenu.SetActive(false);
            retourChapters.SetActive(true);
            verticalPanel.SetActive(false);
            horizontalPanel.SetActive(true);
            gaStuff.SetActive(false);
            if (menucheck.index == 1)
            {
                levelmanager.InitializeSelectionPanel(0);
            }
            else if (menucheck.index == 2)
            {
                levelmanager.InitializeSelectionPanel(1);
            }
            else if (menucheck.index == 3)
            {
                levelmanager.InitializeSelectionPanel(2);
            }
            titleMenu.SetActive(false);
        }
    }
}
