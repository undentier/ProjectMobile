using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSave : MonoBehaviour
{
    public static MenuSave instance;
    public int index;
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
    }
    public void SetInt(int number)
    {
        index = number;
    }
    public void Load()
    {
        titleMenu.SetActive(false);
        playMenu.SetActive(true);
        retourMenu.SetActive(false);
        retourChapters.SetActive(true);
        verticalPanel.SetActive(false);
        horizontalPanel.SetActive(true);
        gaStuff.SetActive(false);
        if(index == 1)
        {
            levelmanager.InitializeSelectionPanel(0);
        }
        else if (index == 2)
        {
            levelmanager.InitializeSelectionPanel(1);
        }
        else if (index == 3)
        {
            levelmanager.InitializeSelectionPanel(2);
        }
    }
}
