using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBlock : MonoBehaviour
{
    public enum ShootType
    {
        None, Lazer, DoubleShoot, Explosion
    }

    public ShootType wichShoot;
    [HideInInspector]
    public int lazer;
    [HideInInspector]
    public int explosion;

    [Header ("Negative effect")]
    public int slowValue;
    public int poisonValue;

    [Header("Stats boost")]
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
            case ShootType.Explosion:
                explosion = 1;
                break;
            default:
                break;
        }
    }
}
