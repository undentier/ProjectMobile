using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scoreOfLastLevel;

    [HideInInspector] public LevelsSO levelScript;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void SaveScore(int _score)
    {
        scoreOfLastLevel = _score;
    }

    public void SetScore()
    {
        if (levelScript.score < scoreOfLastLevel)
        {
            levelScript.score = scoreOfLastLevel;
        }

        InfoReset();
    }

    public void GetLevelInfo(LevelsSO levelDisplayScript)
    {
        levelScript = levelDisplayScript;
    }

    public void InfoReset()
    {
        levelScript = null;
        scoreOfLastLevel = 0;
    }
}
