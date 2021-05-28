using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSave : MonoBehaviour

{
    public static LevelManager levelmanager;
    public LevelManager _levelmanager;
    public static LevelList levellist;
    public LevelList _levellist;

    private void Start()
    {
        levelmanager = _levelmanager;
        levellist = _levellist;
        LoadSave();
        LoadInManager();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            //SaveLevel(levellist.levelsChapter1);
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadInManager();

        }
    }

    public static void LoadInManager()
    {
        List<List<LevelSave>> saves = new List<List<LevelSave>>();
        saves = LoadSave();
        for (int i = 0; i < saves.Count; i++)
        {
            foreach (LevelSave level in saves[i])
            {
                Debug.Log(level.score);
                if(i == 1)
                {
                    levellist.levelsChapter1[level.levelNumber - 1].score = level.score;
                }
                else if( i == 2)
                {
                    levellist.levelsChapter2[level.levelNumber - 1].score = level.score;
                }
                else if (i == 3)
                {
                    levellist.levelsChapter3[level.levelNumber - 1].score = level.score;
                }
            }
        }

        for (int i = 0; i < levelmanager.levelDisplays.Count; i++)
        {
            levelmanager.levelDisplays[i].UpdateScoreDisplay();
        }
    }
    public static void SaveLevel(List<List<LevelsSO>> listSO)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        List<List<LevelSave>> levelsaves = new List<List<LevelSave>>();

        for (int i = 0; i < listSO.Count; i++)
        {
            listSO[i] = new List<LevelsSO>();

            foreach (LevelsSO level in listSO[i])
            {
                LevelSave t = new LevelSave(level);
                levelsaves[i].Add(t);
            }
        }

        formatter.Serialize(stream, levelsaves);
        stream.Close();
    }

    public static List<List<LevelSave>> LoadSave()
    {
        string path = Application.persistentDataPath + "/save.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<List<LevelSave>> saves = formatter.Deserialize(stream) as List<List<LevelSave>>;
            stream.Close();

            return saves;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
