using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header ("Stats")]
    public float speed;
    public int damage;
    public float explosionRadius;

    [Header ("Effect")]
    public GameObject impactEffect;

    [Header("Unity SetUp")]
    public string enemyLayerName;


    private Transform target;
    private LayerMask enemyLayerMask;
    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    private void Start()
    {
        enemyLayerMask = LayerMask.GetMask(enemyLayerName);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    void HitTarget()
    {
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        if (explosionRadius > 0f)
        {
            Explosion();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }

    void Explosion()
    {
        Collider[] enemyInRange = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayerMask);

        foreach (Collider enemyIn in enemyInRange)
        {
            Damage(enemyIn.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
