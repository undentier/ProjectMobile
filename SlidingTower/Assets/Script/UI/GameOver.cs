using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public void TryAgainButton()
   {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
   }
}
