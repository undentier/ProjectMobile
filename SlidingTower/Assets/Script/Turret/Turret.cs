using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
    [Header ("Turret Basic Stats")]
    public float range;
    public float fireRate;
    public float damage;
    public float rotationSpeed;

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

    public LineRenderer laserLineRenderer;

    private float fireCooldown;
    private Transform target;
    private BoostBlock boostScript;
    private GameObject bulletToShoot;
    private Enemy enemyscript;
    private float increseLaserFireRate;

    private void Start()
    {
        bulletToShoot = bulletPrefab;    
    }

    private void Update()
    {
        FindTargetNexus();

        BasiqueTuretSysteme();
    }

    void FindTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Transform enemy in WaveSpawner.enemyList)
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
            for (int i = 0; i < WaveSpawner.enemyList.Count; i++)
            {
                if (WaveSpawner.enemyList[i] != null)
                {
                    if (Vector3.Distance(transform.position, WaveSpawner.enemyList[i].position) < range)
                    {
                        target = WaveSpawner.enemyList[i];
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
        if (!laserLineRenderer.enabled)
        {
            laserLineRenderer.enabled = true;
        }
        laserLineRenderer.SetPosition(0, shootPoint.position);
        laserLineRenderer.SetPosition(1, target.position);

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
            if (laserLineRenderer.enabled)
            {
                laserLineRenderer.enabled = false;
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
