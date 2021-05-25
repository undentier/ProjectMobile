using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager lifeInstance;

    [Header("Stats")]
    public float startLife;
    public int buildToken;

    [HideInInspector]
    public int numOfKill;
    [HideInInspector]
    public float life;

    [Header("GA")]
    public CameraShake cameraShake;
    public Material faisseau;
    public Material nexusEmissive;
    public float powerEmissive;
    public ParticleSystem PVLost;


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
        life = startLife;
        SetupEmissive();
    }

    public void DamagePlayer(int damage)
    {
        life -= damage;

        SetupEmissive();

        if (life <= 0)
        {
            UIManager.instance.gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            StartCoroutine(cameraShake.Shake(0.15f, 2f));
            //PVLost.Play();
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

    void SetupEmissive()
    {
        powerEmissive = life / startLife;

        faisseau.SetFloat("_Opacity", powerEmissive);
        nexusEmissive.SetFloat("_DamageColor", powerEmissive);
    }
}

