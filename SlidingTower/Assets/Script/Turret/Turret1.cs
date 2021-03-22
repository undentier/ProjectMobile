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

    public Enemy[] targets;
    public GameObject basicBullet;

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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
