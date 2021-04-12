using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelecteur : MonoBehaviour
{
    private Touch touch;
    public enum TurretType { BASIC,FIRERATE,DAMAGE,RANGE,POISON,SLOW,EXPLOSION,LASER}
    private TurretType selectedTurret;
    public void BasiqueTurretSelected()
    {
        selectedTurret = TurretType.BASIC;
    }

    public void FireRateBlockSelected()
    {
        selectedTurret = TurretType.FIRERATE;
    }

    public void DamageBlockSelected()
    {
        selectedTurret = TurretType.DAMAGE;
    }

    public void RangeBlockSelected()
    {
        selectedTurret = TurretType.RANGE;
    }

    public void PoisonBlockSelected()
    {
        selectedTurret = TurretType.POISON;
    }

    public void SlowBlockSelected()
    {
        selectedTurret = TurretType.SLOW;
    }

    public void ExplosionBlockSelected()
    {
        selectedTurret = TurretType.EXPLOSION;
    }

    public void LaserBlockSelected()
    {
        selectedTurret = TurretType.LASER;
    }

    private void Update()
    {
        touch = Input.GetTouch(0);
        if(touch.phase == TouchPhase.Ended)
        {
            switch(selectedTurret)
            {
                case TurretType.BASIC:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.basiqueTurretPrefab);
                    break;
                case TurretType.FIRERATE:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.fireRateBlock);
                    break;
                case TurretType.DAMAGE:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.damageBlock);
                    break;
                case TurretType.RANGE:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.rangeBlock);
                    break;
                case TurretType.POISON:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.poisonBlock);
                    break;
                case TurretType.SLOW:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.slowBlock);
                    break;
                case TurretType.EXPLOSION:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.explosionBlock);
                    break;
                case TurretType.LASER:
                    BuildManager.instance.SetTurretToBuild(BuildManager.instance.laserBlock);
                    break;
            }
        }
    }
}
