using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [HideInInspector] public bool gamePause;
    [HideInInspector] public bool enemyPreviewActive;

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
        gamePause = true;
    }



    public void PauseButton()
    {
        gamePause = true;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);

        Time.timeScale = 0f;
    }

    public void OptionButton()
    {

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
        //Time.timeScale = 1f;

        if (GameManager.instance != null)
        {
            GameManager.instance.InfoReset();
        }

        StartCoroutine(WaitFadeEnd("MainMenu_Scene"));
    }
    public void TryAgainButton()
    {
        gamePause = false;
        StartCoroutine(WaitFadeEnd(SceneManager.GetActiveScene().name));
    }


    IEnumerator WaitFadeEnd(string sceneName)
    {
        FadeManager.instance.FadeIn(FadeManager.instance.fadeImage, FadeManager.instance.fadeInTime, true);
        yield return new WaitForSecondsRealtime(FadeManager.instance.fadeInTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
