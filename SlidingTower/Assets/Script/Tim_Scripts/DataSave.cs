using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSave:MonoBehaviour

{
    public LevelManager levelmanager;
    public List<LevelsSO> levelSO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            levelSO = new List<LevelsSO>();
                foreach (LevelsSO level in levelmanager.levels)
                {
                    levelSO.Add(level);
                }            
        }
    }

}
