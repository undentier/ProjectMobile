using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBlock : MonoBehaviour
{
    [Header("Stats boost")]
    public int fireRateBoost;
    public int damageBoost;
    public int rangeBoost;

    [Header ("Negative effect")]
    public int slowValue;
    public int poisonValue;

    [Header ("Shooting type")]
    public int lazer;
    public int explosion;
}
