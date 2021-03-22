using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : MonoBehaviour
{
    public float range;
    public float fireRate;
    public float damage;
    public float rotationSpeed;
    public int numMaxTargets = 5;

    public Transform partToRotate;

    public GameObject basicBullet;
    public Transform target;
    [HideInInspector]
    public Enemy[] targets;

    private GameObject bulletToShoot;
    private List<Enemy> copyList = new List<Enemy>();

    void Start()
    {
        bulletToShoot = basicBullet;
        targets = new Enemy[numMaxTargets];
    }

    void Update()
    {
        FindTargets();

        if (targets[0] != null)
        {
            AimTarget();
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
                    if (Vector3.Distance(transform.position, copyList[i].transform.position) < range)
                    {
                        targets[i] = copyList[i];
                        copyList.Remove(copyList[i]);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
