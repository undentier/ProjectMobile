using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header ("Basique Turret")]
    public GameObject basiqueTurretPrefab;

    [Header ("Boost block")]
    public GameObject fireRateBlock;
    public GameObject damageBlock;
    public GameObject rangeBlock;

    [Space]
    public GameObject poisonBlock;
    public GameObject slowBlock;

    [Space]
    public GameObject explosionBlock;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

}
