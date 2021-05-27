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
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.basiqueTurretPrefab, BuildManager.instance.basiqueTurretPrevisualizePrefab, 0);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void FireRateBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.fireRateBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 1);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void DamageBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.damageBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 2);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void RangeBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.rangeBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 3) ;
            BuildManager.instance.StartDragTurret();
        }
    }

    public void PoisonBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.poisonBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 4);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void SlowBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.slowBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 5);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void ExplosionBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.explosionBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 6);
            BuildManager.instance.StartDragTurret();
        }
    }

    public void LaserBlockSelected()
    {
        if (TouchDetection.currentlyHoveredNode != null)
        {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.laserBlock, BuildManager.instance.basiqueTurretPrevisualizePrefab, 7);
            BuildManager.instance.StartDragTurret();
        }
    }
}
