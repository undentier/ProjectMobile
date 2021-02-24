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
    public bool lazer;
    public bool doubleShoot;
    public bool explosion;

    [Space]
    public BulletType wichBullet;

    public int slowValue;
    public int poisonValue;

    [Space]
    public BoostType wichBoost;

    public int fireRateBoost;
    public int damageBoost;
    public int rangeBoost;

    public void GetShootType(bool _lazer, bool _doubleShoot, bool _explosion)
    {
        _lazer = lazer;
        _doubleShoot = doubleShoot;
        _explosion = explosion;
    }

    public void GetBulletType(BulletType _bulletType, float _slowValue, int _poisonValue)
    {
        _bulletType = wichBullet;
        _slowValue = slowValue;
        _poisonValue = poisonValue;

    }

    public void GetBoostType(int _fireRateBoost, int _damageBoost, int _rangeBoost)
    {
        _fireRateBoost = fireRateBoost;
        _damageBoost = damageBoost;
        _rangeBoost = rangeBoost;
    }
}
