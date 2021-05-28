using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int scoreOfLastLevel;
    public LevelList levellist;

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


        DataSave.SaveLevel(new List<List<LevelsSO>>() { levellist.levelsChapter1, levellist.levelsChapter2, levellist.levelsChapter3});

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
