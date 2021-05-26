using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelecteur : MonoBehaviour
{
    //Changer les arguments des prefabs pour les différentes prévisualisation !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void BasiqueTurretSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.basiqueTurretPrefab, BuildManager.instance.basiqueTurretPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void FireRateBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.fireRateBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void DamageBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.damageBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void RangeBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.rangeBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void PoisonBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.poisonBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void SlowBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.slowBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void ExplosionBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.explosionBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void LaserBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.laserBlock, BuildManager.instance.blockPrevisualizePrefab);
            BuildManager.instance.StartDragTurret();
        }
    }
}
