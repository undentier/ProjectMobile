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
    public Material slowMaterial;
    public Material poisonMaterial;

    public GameObject poisonParticule;

    private Material startMaterial;
    private MeshRenderer rend;

    public float distFromNexus;
    private IEnumerator slowCoroutine;
    private IEnumerator poisonCoroutine;
    private float actualPoisonDuration;
    #endregion

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startMaterial = rend.material;
        actualHealth = startHealth;
        agent.SetDestination(WayPoints.endPoint.position);
        agent.speed = startMovSpeed;
    }

    private void Update()
    {
        distFromNexus = WaveSpawner.instance.GetPathRemainingDistance(agent);

        if (distFromNexus <= 0.5f)
        {
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
        rend.material = poisonMaterial;

        while (actualPoisonDuration > 0)
        {
            yield return new WaitForSeconds(1/poisonTick);
            TakeDamage(poisonDamage);
        }

        rend.material = startMaterial;
    }
    
    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
