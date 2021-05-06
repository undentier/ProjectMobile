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
    public ParticleSystem damageLvl1;
    public ParticleSystem damageLvl2;
    public ParticleSystem damageLvl3;
    private Material shaderMatBullet;

    [ColorUsageAttribute(true, true)]
    private Color actualColor;
    public Color neutralColor;
    public Color slowColor;
    public Color poisonColor;

    [Header("Unity SetUp")]
    public LayerMask enemyLayerMask;

    private Transform target;
    private float distanceThisFrame;
    private Vector3 dir;
    #endregion

    public void Start()
    {
        shaderMatBullet = GetComponent<MeshRenderer>().material;
        actualColor = neutralColor;
    }

    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    public void GetDamage(float _damage)
    {
        damage = _damage;

      /*  Bonjour, ceci est une tentative pour mettre des particules en fonction des dégats
       *  
       *  switch (damageUpgrade)
        {
            case 0:
                shaderMatBullet.SetFloat("PowerFresnel", 1);
                damageLvl1.Stop();
                damageLvl2.Stop();
                damageLvl3.Stop();
                break;
            case 1:
                shaderMatBullet.SetFloat("PowerFresnel", 0.7);
                damageLvl1.Play();
                damageLvl2.Stop();
                damageLvl3.Stop();
                break;
            case 2:
                shaderMatBullet.SetFloat("PowerFresnel", 0.4);
                damageLvl1.Stop();
                damageLvl2.Play();
                damageLvl3.Stop();
                break;
            case 3:
                shaderMatBullet.SetFloat("PowerFresnel", 0.2);
                damageLvl1.Play();
                damageLvl2.Play();
                damageLvl3.Stop();
                break;
            case 4:
                shaderMatBullet.SetFloat("PowerFresnel", 0);
                damageLvl1.Play();
                damageLvl2.Play();
                damageLvl3.Play();
                break;
        }
        */
    }

    public void GetPoisonInfo(float _poisonDamage, float _poisonDuration, float _poisonTick)
    {
        poisonDamage = _poisonDamage;
        poisonDuration = _poisonDuration;
        poisonTick = _poisonTick;

        actualColor = poisonColor;
    }
    public void GetSlowInfo(float _slowForce, float _slowDuration)
    {
        slowForce = _slowForce;
        slowDuration = _slowDuration;

        actualColor = slowColor;
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

        shaderMatBullet.SetVector("_MainColor", actualColor * 3f);

        var main = damageLvl1.main;
        main.startColor = actualColor;
        var main2 = damageLvl2.main;
        main2.startColor = actualColor;
        var main3 = damageLvl3.main;
        main3.startColor = actualColor;
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
