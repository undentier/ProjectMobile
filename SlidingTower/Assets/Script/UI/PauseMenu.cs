using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        Time.timeScale = 0f;
    }
    public void ResumeButton()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

        Time.timeScale = 1f;
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu_Scene");
    }
}
