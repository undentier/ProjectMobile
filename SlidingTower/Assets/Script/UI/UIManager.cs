using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public bool gamePause;

    public GameObject pauseButton;
    public GameObject pauseMenu;

    public GameObject gameOverMenu;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

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


    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
