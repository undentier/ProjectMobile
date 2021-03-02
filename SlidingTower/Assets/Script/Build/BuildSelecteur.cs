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

    public void PoisonBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.poisonBlock);
    }

    public void SlowBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.slowBlock);
    }

    public void ExplosionBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.explosionBlock);
    }

    public void LaserBlockSelected()
    {
        BuildManager.instance.SetTurretToBuild(BuildManager.instance.laserBlock);
    }
}
