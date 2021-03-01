using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header ("Stats")]
    public float speed;
    public float explosionRadius;

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public int slowValue;
    [HideInInspector]
    public int poisonDamage;

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

    public void GetDamage(float _damage)
    {
        damage = _damage;
    }

    public void GetNegatifEffect(int _slowValue, int _poisionDamage)
    {
        slowValue = _slowValue;
        poisonDamage = _poisionDamage;
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
        Destroy(effect, 3f);

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

        if (slowValue > 0)
        {
            e.Slow(slowValue);
        }
        if (poisonDamage > 0)
        {
            e.Poison(poisonDamage);
        }
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
