using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager lifeInstance;

    [Header ("Stats")]
    public int life;

    [Header ("Unity Setup")]
    public Text lifeCounter;
    public GameObject gameOverMenu;

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
        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        lifeCounter.text = life.ToString();
    }

    public void DamagePlayer (int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
