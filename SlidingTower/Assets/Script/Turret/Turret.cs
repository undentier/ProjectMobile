using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Variable
    [Header ("Basic stats")]
    public float range;
    public float fireRate;
    public float damage;
    public float rotationSpeed;
    public int numMaxTargets = 5;
    public int numOfCanon = 1;

    [Header ("Unity setup")]
    public Transform partToRotate;
    public GameObject basicBullet;
    public Transform shootPoint;

    [HideInInspector]
    public Enemy[] targets;
    private GameObject bulletToShoot;
    private List<Enemy> copyList = new List<Enemy>();
    private float fireCoolDown;

    [Header("Upgrade")]
    public int laserUpgrade;
    public int explosionUpgrade;
    [Space]
    public int slowUpgrade;
    public int poisonUpgrade;
    [Space]
    public int fireRateUpgrade;
    public int damageUpgrade;
    public int rangeUpgrade;
    #endregion

    void Start()
    {
        bulletToShoot = basicBullet;
        targets = new Enemy[numMaxTargets];
    }

    void FixedUpdate()
    {
        FindTargets();
        MultiShoot();
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
                        if (Vector3.Distance(transform.position, copyList[i].transform.position) < range)
                        {
                            targets[i] = copyList[i];
                            copyList.Remove(copyList[i]);
                        }
                    }
                }
            }
            if (targets[i] != null)
            {
                if (Vector3.Distance(transform.position ,targets[i].transform.position) > range)
                {
                    targets[i] = null;
                }
            }
        }
    }
    void AimTarget()
    {
        Vector3 dir = targets[0].transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Fire(Enemy target)
    {
        GameObject actualBullet = Instantiate(bulletToShoot, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = actualBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.GetTarget(target.transform);
            bulletScript.GetDamage(damage);
        }
    }

    void MultiShoot()
    {
        if (targets[0] != null)
        {
            AimTarget();

            if (fireCoolDown <= 0f)
            {
                for (int i = 0; i < numOfCanon; i++)
                {
                    if (targets[i] != null)
                    {
                        Fire(targets[i]);
                    }
                }
                fireCoolDown = 1f / fireRate;
            }
            fireCoolDown -= Time.deltaTime;
        }
    }
 
    public void GetNodeUpgrade(NodeSysteme node)
    {
        Debug.Log("Upgrade");
        laserUpgrade = node.laserUpgrade;
        explosionUpgrade = node.explosionUpgrade;

        slowUpgrade = node.slowUpgrade;
        poisonUpgrade = node.poisonUpgrade;

        fireRateUpgrade = node.fireRateUpgrade;
        damageUpgrade = node.damageUpgrade;
        rangeUpgrade = node.rangeUpgrade;
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
