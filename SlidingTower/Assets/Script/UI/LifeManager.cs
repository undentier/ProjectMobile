using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager lifeInstance;

    [Header("Stats")]
    public int life;
    public int buildToken;

    [HideInInspector]
    public int numOfKill;
    [HideInInspector]
    public int startLife;

    [Header("GA")]
    public CameraShake cameraShake;

    private void Awake()
    {
        if (lifeInstance != null)
        {
            Debug.Log("Double manager attention");
            return;
        }
        else
        {
            lifeInstance = this;
        }
    }

    private void Start()
    {
        startLife = life;
    }

    public void DamagePlayer(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            UIManager.instance.gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            StartCoroutine(cameraShake.Shake(0.15f, 2f));
        }
    }

    public void ChangeToken(int tokenNumber)
    {
        buildToken += tokenNumber;

        if (buildToken <= 0)
        {
            WavePanel.instance.DisableBuildMode();
        }
    }

    public void AddKillScore(int score)
    {
        numOfKill += score;
    }
}
