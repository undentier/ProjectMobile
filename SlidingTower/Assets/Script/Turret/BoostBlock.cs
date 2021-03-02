using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBlock : MonoBehaviour
{
    public enum ShootType
    {
        None, Lazer, DoubleShoot, Explosion
    }

    public enum BulletType
    {
        None, Slow, Poison
    }

    public enum BoostType
    {
        None, FireRate, Damage, Range
    }


    public ShootType wichShoot;
    [HideInInspector]
    public int lazer;
    [HideInInspector]
    public int doubleShoot;
    [HideInInspector]
    public int explosion;

    [Space]
    public BulletType wichBullet;

    public int slowValue;
    public int poisonValue;

    [Space]
    public BoostType wichBoost;

    public int fireRateBoost;
    public int damageBoost;
    public int rangeBoost;

    private void Start()
    {
        switch (wichShoot)
        {
            case ShootType.None:
                break;
            case ShootType.Lazer:
                lazer = 1;
                break;
            case ShootType.DoubleShoot:
                doubleShoot = 1;
                break;
            case ShootType.Explosion:
                explosion = 1;
                break;
            default:
                break;
        }
    }
}
