using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave:MonoBehaviour

{
    public List<LevelManager> levelManagers ;
    public List<LevelsSO> levelSO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < levelManagers.Count; i++)
            {
                foreach (LevelDisplay level in levelManagers[i].levels)
                {
                    levelSO.Add(level.level);
                }
            }
        }
    }

}
