using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelecteur : MonoBehaviour
{

    private void Start()
    {
    }

    public void BasiqueTurretSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.basiqueTurretPrefab);
    }

    public void SpeedBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.speedBlock);
    }
}
