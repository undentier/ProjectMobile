using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public MenuSave menusave;
   public void TryAgainButton()
   {
        Time.timeScale = 1f;
        MenuSave.instance.Load();       
        SceneManager.LoadScene(0);
   }
}
