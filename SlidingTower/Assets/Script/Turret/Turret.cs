using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
    #region Variable
    [Header ("Turret Basic Stats")]
    public float range;
    public float fireRate;
    public float damage;
    public float rotationSpeed;
    public int maxLaser;

    [Header("Upgrade Stats")]

    public int lazerUpgrade;
    public int doubleShootUpgrade;
    public int explosionUpgrade;

    [Space]

    public int slowValueUpgrade;
    public int poisonValueUpgrade;

    [Header ("Unity Setup")]
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public Transform shootPoint;

    public LineRenderer[] laserLineRenderers;

    private float fireCooldown;
    private Transform target;
    public Transform[] targets;
    private BoostBlock boostScript;
    private GameObject bulletToShoot;
    private Enemy enemyscript;
    private float increseLaserFireRate;
    #endregion

    private void Start()
    {
        bulletToShoot = bulletPrefab;
        for (int i = 0; i < laserLineRenderers.Length; i++)
        {
            laserLineRenderers[i].enabled = false;
        }
    }

    private void Update()
    {
        FindTargetNexus();
        FindMultipleTarget();

        BasiqueTuretSysteme();
    }

    void FindTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Transform enemy in WaveSpawner.instance.enemyList)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void FindTargetNexus()
    {
        if (target == null)
        {
            for (int i = 0; i < WaveSpawner.instance.enemyList.Count; i++)
            {
                if (WaveSpawner.instance.enemyList[i] != null)
                {
                    if (Vector3.Distance(transform.position, WaveSpawner.instance.enemyList[i].position) < range)
                    {
                        target = WaveSpawner.instance.enemyList[i];
                        break;
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) > range)
            {
                target = null;
            }
        }
    }

    void FindMultipleTarget()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
            {
                if (Vector3.Distance(transform.position, targets[i].transform.position) > range)
                {
                    targets[i] = null;
                }
            }
        }

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
            {
                for (int r = i; r < WaveSpawner.instance.enemyList.Count; r++)
                {
                    if (WaveSpawner.instance.enemyList[r] != null)
                    {
                        if (Vector3.Distance(transform.position, WaveSpawner.instance.enemyList[r].transform.position) < range)
                        {
                            targets[i] = WaveSpawner.instance.enemyList[r];
                        }
                    }
                }
            }
        }
    }

    void AimTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Shoot()
    {
        GameObject actualBullet =  Instantiate(bulletToShoot, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = actualBullet.GetComponent<Bullet>();
        
        if (bulletScript != null)
        {
            bulletScript.GetTarget(target);
            bulletScript.GetDamage(damage);
            bulletScript.GetNegatifEffect(slowValueUpgrade, poisonValueUpgrade);
        }
    }

    void Laser()
    {
        if (!laserLineRenderers[0].enabled)
        {
            laserLineRenderers[0].enabled = true;
        }
        laserLineRenderers[0].SetPosition(0, shootPoint.position);
        laserLineRenderers[0].SetPosition(1, target.position);

        if (target != null)
        {
            if (enemyscript == null)
            {
                enemyscript = target.GetComponent<Enemy>();
                increseLaserFireRate = 1f;
            }
            else
            {
                increseLaserFireRate += Time.deltaTime * 3;

                if (slowValueUpgrade > 0)
                {
                    enemyscript.Slow(slowValueUpgrade);
                }
                if (poisonValueUpgrade > 0)
                {
                    enemyscript.Poison(poisonValueUpgrade);
                }

                if (fireCooldown <= 0f)
                {
                    enemyscript.TakeDamage(damage/2);
                    fireCooldown = 1 / (fireRate * increseLaserFireRate);
                }
                fireCooldown -= Time.deltaTime;
            }
        }
    }

    void MultiLaser()
    {
        
    }

    void CheckBulletType()
    {
        if (explosionUpgrade > 0)
        {
            bulletToShoot = missilePrefab;
        }
        else
        {
            bulletToShoot = bulletPrefab;
        }
    }

    void BasiqueTuretSysteme()
    {
        CheckBulletType();

        if (target == null)
        {
            if (laserLineRenderers[0].enabled)
            {
                laserLineRenderers[0].enabled = false;
            }
            return;
        }
        else
        {
            AimTarget();
        }

        if (lazerUpgrade > 0)
        {
            Laser();
        }
        else
        {
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }
            fireCooldown -= Time.deltaTime;
        }

    }

    #region BoostSysteme
    void GetUpgrade()
    {
        lazerUpgrade += boostScript.lazer;
        doubleShootUpgrade += boostScript.doubleShoot;
        explosionUpgrade += boostScript.explosion;

        slowValueUpgrade += boostScript.slowValue;
        poisonValueUpgrade += boostScript.poisonValue;

        fireRate += boostScript.fireRateBoost;
        damage += boostScript.damageBoost;
        range += boostScript.rangeBoost;
    }
    void DelUpgrade()
    {
        lazerUpgrade -= boostScript.lazer;
        doubleShootUpgrade -= boostScript.doubleShoot;
        explosionUpgrade -= boostScript.explosion;

        slowValueUpgrade -= boostScript.slowValue;
        poisonValueUpgrade -= boostScript.poisonValue;

        fireRate -= boostScript.fireRateBoost;
        damage -= boostScript.damageBoost;
        range -= boostScript.rangeBoost;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            boostScript = other.GetComponent<BoostBlock>();
            GetUpgrade();
            boostScript = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            boostScript = other.GetComponent<BoostBlock>();
            DelUpgrade();
            boostScript = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
