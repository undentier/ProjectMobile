using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelecteur : MonoBehaviour
{
    public void BasiqueTurretSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.basiqueTurretPrefab);
    }

    public void FireRateBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.fireRateBlock);
    }

    public void DamageBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.damageBlock);
    }

    public void RangeBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.rangeBlock);
    }
}
