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
            SaveLevel(levellist.levelsChapter1);
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadInManager();
            Debug.Log("Loaded");
        }
    }

    public static void LoadInManager()
    {
        List<LevelSave> saves = new List<LevelSave>();
        saves = LoadSave();
        foreach (LevelSave level in saves)
        {
            levelmanager.levelsChapter1[level.levelNumber - 1].score = level.score;
        }

        for (int i = 0; i < levelmanager.levelDisplays.Count; i++)
        {
            levelmanager.levelDisplays[i].UpdateScoreDisplay();
        }
    }
    public static void SaveLevel(List<LevelsSO> listSO)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        List<LevelSave> levelsaves = new List<LevelSave>();
        foreach (LevelsSO level in listSO)
        {
            levelsaves.Add(new LevelSave(level));
        }

        formatter.Serialize(stream, levelsaves);
        stream.Close();
        Debug.Log(path);
    }

    public static List<LevelSave> LoadSave()
    {
        string path = Application.persistentDataPath + "/save.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<LevelSave> saves = formatter.Deserialize(stream) as List<LevelSave>;
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
