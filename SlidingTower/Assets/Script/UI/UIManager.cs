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
    public GameObject victoryMenu;

    public EnemyPreview previewScript;
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


    public void DisplayVictoryMenu()
    {
        victoryMenu.SetActive(true);
    }



    public void PauseButton()
    {
        gamePause = true;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        Time.timeScale = 0f;
    }
    public void ResumeButton()
    {
        gamePause = false;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);

        Time.timeScale = 1f;
    }
    public void MainMenuButton()
    {
        gamePause = false;
        Time.timeScale = 1f;

        if (GameManager.instance != null)
        {
            GameManager.instance.InfoReset();
        }

        SceneManager.LoadScene("MainMenu_Scene");
    }
    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

}
