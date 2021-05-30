using UnityEngine;
using System.Collections;

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
    //public ParticleSystem PVLost;
    public ParticleSystem decoNexus;
    public GameObject kaBoom;

    private bool lockDeath;


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
        decoNexus.startLifetime = 3f;
        kaBoom.SetActive(false);
    }

    public void DamagePlayer(int damage)
    {
        life -= damage;
        PlayerSoundManager.I.PlayDamageNexus(0.5f);
        SetupEmissive();
        decoNexus.startLifetime = 4f * life / startLife;

        if (life <= 0)
        {
            if (!lockDeath)
            {
                StartCoroutine(WaitBeforeEnd());
            }
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

    IEnumerator WaitBeforeEnd()
    {
        lockDeath = true;
        kaBoom.SetActive(true);
        yield return new WaitForSeconds(4f);
        UIManager.instance.gameOverMenu.SetActive(true);
        kaBoom.SetActive(false);
        Time.timeScale = 0f;
    }
}

