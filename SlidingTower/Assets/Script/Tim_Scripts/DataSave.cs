﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSave : MonoBehaviour

{
    public static DataSave instance;
    public LevelManager levelmanager;
    public List<LevelsSO> levelSO;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            levelSO = new List<LevelsSO>();
            foreach (LevelsSO level in levelmanager.levels)
            {
                levelSO.Add(level);
            }

            SaveLevel(levelSO);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadSave();
            Debug.Log("A");
        }
    }

    public static void SaveLevel(List<LevelsSO> listSO)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        foreach (LevelsSO level in listSO)

        {
            LevelSave save = new LevelSave(level);
            formatter.Serialize(stream, save);
        }

        stream.Close();
        Debug.Log(path);
    }

    public static LevelSave LoadSave()
    {
        string path = Application.persistentDataPath + "/save.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelSave save = formatter.Deserialize(stream) as LevelSave;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
