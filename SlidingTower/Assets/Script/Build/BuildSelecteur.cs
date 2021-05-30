using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelecteur : MonoBehaviour
{
    //Changer les arguments des prefabs pour les différentes prévisualisation !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void BasiqueTurretSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.basiqueTurretPrefab, BuildManager.instance.basiqueTurretPrevisualizePrefab, 0, true);
            BuildManager.instance.StartDragTurret();
        
    }

    public void FireRateBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.fireRateBlock, BuildManager.instance.blockPrevisualizePrefab, 1, false);
            BuildManager.instance.StartDragTurret();
    }

    public void DamageBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.damageBlock, BuildManager.instance.blockPrevisualizePrefab, 2, false);
            BuildManager.instance.StartDragTurret();
    }

    public void RangeBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.rangeBlock, BuildManager.instance.blockPrevisualizePrefab, 3, false) ;
            BuildManager.instance.StartDragTurret();
    }

    public void PoisonBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.poisonBlock, BuildManager.instance.blockPrevisualizePrefab, 4, false);
            BuildManager.instance.StartDragTurret();
    }

    public void SlowBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.slowBlock, BuildManager.instance.blockPrevisualizePrefab, 5, false);
            BuildManager.instance.StartDragTurret();
    }

    public void ExplosionBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.explosionBlock, BuildManager.instance.blockPrevisualizePrefab, 6, false);
            BuildManager.instance.StartDragTurret();
    }

    public void LaserBlockSelected()
    {
            PlayerSoundManager.I.SelectTurret(1);
            BuildManager.instance.SetTurretToBuild(BuildManager.instance.laserBlock, BuildManager.instance.blockPrevisualizePrefab, 7, false);
            BuildManager.instance.StartDragTurret();
    }
}
