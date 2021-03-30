using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Variable
    [Header ("Basic stats")]
    public float startFireRate = 3;
    public float startDamage;
    public float startRange;
    public float rotationSpeed;
    public int numMaxTargets = 5;

    [Header ("Value of stat upgrade")]
    public float[] fireRateBonus;
    public int[] damageBonus;
    public float[] rangeBonus;

    private float actualFireRate;
    private float actualDamage;
    private float actualRange;

    [Header ("Value of negatif effect")]
    public float[] slowForceBonus;
    public float[] slowDurationBonus;

    private float actualSlowForce;
    private float actualSlowDuration;
    [Space]
    public float[] poisonDamageBonus;
    public float[] poisonDurationBonus;
    public float[] poisonTickBonus;

    private float actualpoisonDamage;
    private float actualPoisonDuration;
    private float actualPoisonTick;

    [Header("Value of shooting type")]
    public float[] explosionRadiusBonus;
    public int[] numOfCanonBonus;

    private float actualExplosionRadius;
    private int actualNumOfCanon;
    [Space]
    public float[] laserDamageReductionBonus;
    public float[] laserFirerateMultiplierBonus;
    public float[] microLaserDamageReductionBonus;

    private float actualLaserDamageReduction;
    private float actualLaserFireRateMultiplier;
    private float actualMicroLaserDamageReduction;
    

    [Header ("Unity setup")]
    public Transform partToRotate;
    public GameObject basicBullet;
    public GameObject explosiveBullet;
    public Transform shootPoint;
    public LineRenderer[] laserLines;

    [HideInInspector]
    public Enemy[] targets;
    private GameObject bulletToShoot;
    private List<Enemy> copyList = new List<Enemy>();
    private float fireCoolDown;
    private float[] laserMultiplier;
    private float[] laserCoolDown;

    #region Upgrade variable
    [HideInInspector]
    public int laserUpgrade;
    [HideInInspector]
    public int explosionUpgrade;

    [HideInInspector]
    public int slowUpgrade;
    [HideInInspector]
    public int poisonUpgrade;

    [HideInInspector]
    public int fireRateUpgrade;
    [HideInInspector]
    public int damageUpgrade;
    [HideInInspector]
    public int rangeUpgrade;
    #endregion

    #endregion

    void Awake()
    {
        bulletToShoot = basicBullet;
        targets = new Enemy[numMaxTargets];
        laserMultiplier = new float[numMaxTargets];
        laserCoolDown = new float[numMaxTargets];
    }

    void FixedUpdate()
    {
        FindTargets();

        if (laserUpgrade > 0)
        {
            Laser();
        }
        else
        {
            MultiShoot();
        }
    }

    void FindTargets()
    {
        copyList = new List<Enemy>(WaveSpawner.instance.enemyList);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
            {
                if (copyList.Count >= i + 1)
                {
                    if (copyList[i] != null)
                    {
                        if (Vector3.Distance(transform.position, copyList[i].transform.position) < actualRange)
                        {
                            targets[i] = copyList[i];
                            copyList.Remove(copyList[i]);
                        }
                    }
                }
            }
            if (targets[i] != null)
            {
                if (Vector3.Distance(transform.position ,targets[i].transform.position) > actualRange)
                {
                    targets[i] = null;
                }
            }
        }
    }

    void MultiShoot()
    {
        if (targets[0] != null)
        {
            AimTarget();

            if (fireCoolDown <= 0f)
            {
                Fire(targets[0]);
                fireCoolDown = 1f / actualFireRate;
            }
            fireCoolDown -= Time.deltaTime;
        }
    }
    void Laser()
    {
        AimTarget();

        for (int i = 0; i < actualNumOfCanon; i++)
        {
            if (targets[i] != null)
            {
                laserLines[i].enabled = true;
                laserLines[i].SetPosition(0, shootPoint.position);
                laserLines[i].SetPosition(1, targets[i].transform.position);

                if (actualNumOfCanon > 1)
                {
                    laserMultiplier[i] += Time.deltaTime * (actualLaserFireRateMultiplier / actualMicroLaserDamageReduction);
                }
                else
                {
                    laserMultiplier[i] += Time.deltaTime * actualLaserFireRateMultiplier;

                    for (int t = 1; t < laserLines.Length; t++)
                    {
                        laserLines[t].enabled = false;
                    }
                }

                if (laserCoolDown[i] <= 0f)
                {
                    if (slowUpgrade > 0)
                    {
                        targets[i].StartSlow(actualSlowForce, actualSlowDuration);
                    }
                    if (poisonUpgrade > 0)
                    {
                        targets[i].Poison(actualpoisonDamage, actualPoisonDuration, actualPoisonTick);
                    }
                    targets[i].TakeDamage(actualDamage / actualLaserDamageReduction);
                    laserCoolDown[i] = 1 / (actualFireRate * laserMultiplier[i]);
                }
                laserCoolDown[i] -= Time.deltaTime;
            }
            else
            {
                laserLines[i].enabled = false;
                laserMultiplier[i] = 1f;
                laserCoolDown[i] = 0f;
            }
        }
    }

    void AimTarget()
    {
        if (targets[0] != null)
        {
            Vector3 dir = targets[0].transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }
    void Fire(Enemy target)
    {
        GameObject actualBullet = Instantiate(bulletToShoot, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = actualBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.GetTarget(target.transform);
            bulletScript.GetDamage(actualDamage);
            bulletScript.GetSlowInfo(actualSlowForce, actualSlowDuration);
            bulletScript.GetPoisonInfo(actualpoisonDamage, actualPoisonDuration, actualPoisonTick);
            bulletScript.GetExplosiveInfo(actualExplosionRadius);
        }
    }

    public void GetNodeUpgrade(NodeSysteme node)
    {
        laserUpgrade = node.laserUpgrade;
        explosionUpgrade = node.explosionUpgrade;

        slowUpgrade = node.slowUpgrade;
        poisonUpgrade = node.poisonUpgrade;

        fireRateUpgrade = node.fireRateUpgrade;
        damageUpgrade = node.damageUpgrade;
        rangeUpgrade = node.rangeUpgrade;

        ApplyUpgrade();
    }
    public void ResetUpgrade()
    {
        laserUpgrade = 0;
        explosionUpgrade = 0;

        slowUpgrade = 0;
        poisonUpgrade = 0;

        fireRateUpgrade = 0;
        damageUpgrade = 0;
        rangeUpgrade = 0;

        ApplyUpgrade();
    }

    void ApplyUpgrade()
    {
        #region Stats boost
        switch (fireRateUpgrade)
        {
            case 0:
                actualFireRate = startFireRate;
                break;
            case 1:
                actualFireRate = fireRateBonus[0];
                break;
            case 2:
                actualFireRate = fireRateBonus[1];
                break;
            case 3:
                actualFireRate = fireRateBonus[2];
                break;
            case 4:
                actualFireRate = fireRateBonus[3];
                break;
        }

        switch (damageUpgrade)
        {
            case 0:
                actualDamage = startDamage;
                break;
            case 1:
                actualDamage = damageBonus[0];
                break;
            case 2:
                actualDamage = damageBonus[1];
                break;
            case 3:
                actualDamage = damageBonus[2];
                break;
            case 4:
                actualDamage = damageBonus[3];
                break;
        }

        switch (rangeUpgrade)
        {
            case 0:
                actualRange = startRange;
                break;
            case 1:
                actualRange = rangeBonus[0];
                break;
            case 2:
                actualRange = rangeBonus[1];
                break;
            case 3:
                actualRange = rangeBonus[2];
                break;
            case 4:
                actualRange = rangeBonus[3];
                break;
        }
        #endregion

        #region Negatif effect boost
        switch (slowUpgrade)
        {
            case 0:
                actualSlowForce = 0;
                actualSlowDuration = 0;
                break;
            case 1:
                actualSlowForce = slowForceBonus[0];
                actualSlowDuration = slowDurationBonus[0];
                break;
            case 2:
                actualSlowForce = slowForceBonus[1];
                actualSlowDuration = slowDurationBonus[1];
                break;
            case 3:
                actualSlowForce = slowForceBonus[2];
                actualSlowDuration = slowDurationBonus[2];
                break;
            case 4:
                actualSlowForce = slowForceBonus[3];
                actualSlowDuration = slowDurationBonus[3];
                break;
        }

        switch (poisonUpgrade)
        {
            case 0:
                actualpoisonDamage = 0f;
                actualPoisonDuration = 0f;
                actualPoisonTick = 0f;
                break;
            case 1:
                actualpoisonDamage = poisonDamageBonus[0];
                actualPoisonDuration = poisonDurationBonus[0];
                actualPoisonTick = poisonTickBonus[0];
                break;
            case 2:
                actualpoisonDamage = poisonDamageBonus[1];
                actualPoisonDuration = poisonDurationBonus[1];
                actualPoisonTick = poisonTickBonus[1];
                break;
            case 3:
                actualpoisonDamage = poisonDamageBonus[2];
                actualPoisonDuration = poisonDurationBonus[2];
                actualPoisonTick = poisonTickBonus[2];
                break;
            case 4:
                actualpoisonDamage = poisonDamageBonus[3];
                actualPoisonDuration = poisonDurationBonus[3];
                actualPoisonTick = poisonTickBonus[3];
                break;
        }
        #endregion

        #region Shoot type boost
        switch (explosionUpgrade)
        {
            case 0:
                bulletToShoot = basicBullet;
                actualExplosionRadius = 0f;
                actualNumOfCanon = 1;
                break;
            case 1:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[0];
                actualNumOfCanon = numOfCanonBonus[0];
                break;
            case 2:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[1];
                actualNumOfCanon = numOfCanonBonus[1];
                break;
            case 3:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[2];
                actualNumOfCanon = numOfCanonBonus[2];
                break;
            case 4:
                bulletToShoot = explosiveBullet;
                actualExplosionRadius = explosionRadiusBonus[3];
                actualNumOfCanon = numOfCanonBonus[3];
                break;
        }

        switch (laserUpgrade)
        {
            case 0:
                actualLaserDamageReduction = 0;
                actualLaserFireRateMultiplier = 0;
                actualMicroLaserDamageReduction = 0;
                ResetLaser();
                break;
            case 1:
                actualLaserDamageReduction = laserDamageReductionBonus[0];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[0];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[0];
                break;
            case 2:
                actualLaserDamageReduction = laserDamageReductionBonus[1];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[1];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[1];
                break;
            case 3:
                actualLaserDamageReduction = laserDamageReductionBonus[2];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[2];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[2];
                break;
            case 4:
                actualLaserDamageReduction = laserDamageReductionBonus[3];
                actualLaserFireRateMultiplier = laserFirerateMultiplierBonus[3];
                actualMicroLaserDamageReduction = microLaserDamageReductionBonus[3];
                break;
        }
        #endregion
    }

    void ResetLaser()
    {
        if (laserUpgrade < 1)
        {
            for (int i = 0; i < laserLines.Length; i++)
            {
                laserLines[i].enabled = false;
                laserMultiplier[i] = 1f;
                laserCoolDown[i] = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, actualRange);
    }
}
