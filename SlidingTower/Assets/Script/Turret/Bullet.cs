using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variable
    [Header ("Stats")]
    public float speed;
    public float explosionRadius;

    [HideInInspector]
    public float damage;
    public float slowForce;
    public float slowDuration;
    [HideInInspector]
    public int poisonDamage;

    [Header ("Effect")]
    public GameObject impactEffect;

    [Header("Unity SetUp")]
    public LayerMask enemyLayerMask;

    private Transform target;
    private float distanceThisFrame;
    private Vector3 dir;
    #endregion

    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    public void GetDamage(float _damage)
    {
        damage = _damage;
    }

    public void GetNegatifEffect(int _poisionDamage)
    {
        poisonDamage = _poisionDamage;
    }
    public void GetSlowInfo(float _slowForce, float _slowDuration)
    {
        slowForce = _slowForce;
        slowDuration = _slowDuration;
    }

   
    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        } // Auto destroy if no more target

        Mouvement(); 

        if (dir.magnitude <= distanceThisFrame) 
        {
            HitTarget();
        } // If the bullet touch the target
    }

    void Mouvement()
    {
        dir = target.position - transform.position;
        distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    } // Translate the bullet to the target
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
    } // Call when bullet hit the target

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);

        if (slowForce > 0)
        {
            e.StartSlow(slowForce, slowDuration);
        }
        if (poisonDamage > 0)
        {
            e.Poison(poisonDamage);
        }
    } // Apply damage and negatif effect on target

    void Explosion()
    {
        Collider[] enemyInRange = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayerMask);

        foreach (Collider enemyIn in enemyInRange)
        {
            Damage(enemyIn.transform);
        }
    } // If explosion radius > 0 Create a sphere detection around the target and apply damage and negatif effect to all enemies 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
