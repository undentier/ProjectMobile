using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variable
    [Header ("Stats")]
    public float speed;

    private float damage;

    private float slowForce;
    private float slowDuration;

    private float poisonDamage;
    private float poisonDuration;
    private float poisonTick;

    private float explosionRadius;

    [Header ("Effect")]
    public GameObject impactEffect;
    public GameObject impactEffectSlow;
    public GameObject impactEffectPoison;
    public GameObject impactEffectSP;
    public GameObject impactSlowUp;
    public GameObject impactPoisonUp;

    public ParticleSystem damageLvl1;
    public ParticleSystem damageLvl2;
    public ParticleSystem damageLvl3;

    private Material shaderMatBullet;
    public GameObject Renderer;

    [Header("Unity SetUp")]
    public LayerMask enemyLayerMask;

    private Transform target;
    private float distanceThisFrame;
    private Vector3 dir;
    #endregion

    public void Start()
    {
        shaderMatBullet = Renderer.GetComponent<MeshRenderer>().material;
        
        if (slowForce == 0 && poisonDamage == 0)
        { shaderMatBullet.SetFloat("inputColor", 0); }

        if (slowForce > 0 && poisonDamage == 0)
        { shaderMatBullet.SetFloat("inputColor", 1); }

        if (poisonDamage > 0 && slowForce == 0)
        { shaderMatBullet.SetFloat("inputColor", 2); }

        if (poisonDamage > 0 && slowForce > 0)
        { shaderMatBullet.SetFloat("inputColor", 3); }
    }

    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    public void GetDamage(float _damage)
    {
        damage = _damage;      
    }
                    

    public void GetPoisonInfo(float _poisonDamage, float _poisonDuration, float _poisonTick)
    {
        poisonDamage = _poisonDamage;
        poisonDuration = _poisonDuration;
        poisonTick = _poisonTick;
    }

    public void GetSlowInfo(float _slowForce, float _slowDuration)
    {
        slowForce = _slowForce;
        slowDuration = _slowDuration;
    }

    public void GetExplosiveInfo(float _explosionRadius)
    {
        explosionRadius = _explosionRadius;
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

        if (damage == 30)
        {
            shaderMatBullet.SetFloat("powerFresnel", 1.5f);
            damageLvl1.Stop();
            damageLvl2.Stop();
            damageLvl3.Stop();
        }

        if (damage == 50)
        {
            shaderMatBullet.SetFloat("powerFresnel", 1.25f);
            damageLvl1.Play();
            damageLvl2.Stop();
            damageLvl3.Stop();
        }

        if (damage == 60)
        {
            shaderMatBullet.SetFloat("powerFresnel", 1);
            damageLvl1.Stop();
            damageLvl2.Play();
            damageLvl3.Stop();
        }

        if (damage == 90)
        {
            shaderMatBullet.SetFloat("powerFresnel", 0);
            damageLvl1.Play();
            damageLvl2.Play();
            damageLvl3.Stop();
        }

        if (damage == 130)
        {
            shaderMatBullet.SetFloat("powerFresnel", 0);
            damageLvl1.Play();
            damageLvl2.Play();
            damageLvl3.Play();
        }

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
        if (explosionRadius > 0f)
        {
            Explosion();
        }
        else
        {
            Damage(target);
        }

        if (slowForce == 0 && poisonDamage == 0)
        {
            GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effect, 3f);
        }
        if (slowForce > 0 && poisonDamage == 0 )
        {
            GameObject effectSlow = Instantiate(impactEffectSlow, transform.position, transform.rotation);
            Destroy(effectSlow, 3f);
        }
        if (poisonDamage > 0 && slowForce == 0 )
        {
            GameObject effectPoison = Instantiate(impactEffectPoison, transform.position, transform.rotation);
            Destroy(effectPoison, 3f);
        }
        if (poisonDamage > 0 && slowForce > 0)
        {
            GameObject effectSP = Instantiate(impactEffectSP, transform.position, transform.rotation);
            Destroy(effectSP, 3f);
        }
        if(poisonDamage > 2)
        {
            GameObject effectPoisonUp = Instantiate(impactPoisonUp, transform.position, transform.rotation);
            Destroy(effectPoisonUp, 3f);
        }
        if(slowForce > 2)
        {
            GameObject effectSlowUp = Instantiate(impactSlowUp, transform.position, transform.rotation);
            Destroy(effectSlowUp, 3f);
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
            e.Poison(poisonDamage, poisonDuration, poisonTick);
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
