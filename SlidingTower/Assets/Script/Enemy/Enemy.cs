using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Variable
    [Header ("Stats")]
    private float actualMovSpeed;
    public float startMovSpeed;
    public float startHealth;
    [HideInInspector]
    public float actualHealth;
    public int damageToNexus;
    
    [Header ("Unity Setup")]
    public NavMeshAgent agent;
    public GameObject deathEffect;
    public Image healhtBar;

    [Header("Effect")]
    public GameObject ennemy;
    public Material slowMaterial;
    public Material poisonMaterial;

    public ParticleSystem poisonParticule;
    public GameObject damageNexus;

    private Material startMaterial;
    private SkinnedMeshRenderer rend;

    public float distFromNexus;
    private IEnumerator slowCoroutine;
    private IEnumerator poisonCoroutine;
    private float actualPoisonDuration;

    public GameObject EnnemyDead;
    #endregion

    private void Start()
    {
        rend = ennemy.GetComponent<SkinnedMeshRenderer>();
        startMaterial = rend.material;
        actualHealth = startHealth;
        agent.SetDestination(EndInfo.endPoint.position);
        agent.speed = startMovSpeed;
        actualMovSpeed = startMovSpeed;
    }

    private void Update()
    {
        distFromNexus = GetPathRemainingDistance(agent);

        if (distFromNexus <= 0.5f)
        {
            GameObject destruction = Instantiate(damageNexus, transform.position, transform.rotation);
            Destroy(destruction, 1f);

            LifeManager.lifeInstance.DamagePlayer(damageToNexus);
            Destroy(gameObject);
            
            return;
        }

        if (actualPoisonDuration > 0)
        {
            actualPoisonDuration -= Time.deltaTime;
        }
    }

    public void TakeDamage(float amount)
    {
        actualHealth -= amount;
        healhtBar.fillAmount = actualHealth / startHealth;
        EnemySoundManager.I.Hurt(0.4f);

        if (actualHealth <= 0f)
        {
            Die();
        }
    }

    public void StartSlow(float _slowForce, float _slowDuration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }
        slowCoroutine = ApplySLow(_slowForce, _slowDuration);
        StartCoroutine(slowCoroutine);
    }

    IEnumerator ApplySLow(float slowForce, float slowDuration)
    {
        rend.material = slowMaterial;
        agent.speed = actualMovSpeed - slowForce;
        yield return new WaitForSeconds(slowDuration);
        actualMovSpeed = startMovSpeed;
        rend.material = startMaterial;
    }


    public void Poison(float _poisonDamage, float _poisonDuration, float _poisonTick)
    {
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
        }

        poisonCoroutine = ApplyPoison(_poisonDamage, _poisonDuration, _poisonTick);
        StartCoroutine(poisonCoroutine);
        
    }

    IEnumerator ApplyPoison(float poisonDamage, float poisonDuration, float poisonTick)
    {
        actualPoisonDuration = poisonDuration;
        //rend.material = poisonMaterial;
        poisonParticule.Play();

        while (actualPoisonDuration > 0)
        {
            yield return new WaitForSeconds(1/poisonTick);
            TakeDamage(poisonDamage);
        }

        //rend.material = startMaterial;
        poisonParticule.Stop();
    }
    
    void Die()
    {
        EnemySoundManager.I.Death(2);
        LifeManager.lifeInstance.AddKillScore(1);
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);

        GameObject animMort = Instantiate(EnnemyDead, transform.position, transform.rotation);
        Destroy(animMort, 2f);
        Destroy(gameObject);
    }

    public float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }
}
