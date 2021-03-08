using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Variable
    [Header ("Stats")]
    public float movSpeed;
    private float startMovSpeed;
    public float startHealth;
    [HideInInspector]
    public float actualHealth;
    public int damageToNexus;

    public float slowTime;
    public int poisonDuration;

    [Header ("Unity Setup")]
    public NavMeshAgent agent;
    public GameObject deathEffect;
    public Image healhtBar;

    [Header("Effect")]
    public Material slowMaterial;
    public Material poisionMaterial;

    public GameObject poisonParticule;

    private Material startMaterial;
    private MeshRenderer rend;

    private bool isPoison;
    public float distFromNexus;
    #endregion

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startMaterial = rend.material;
        actualHealth = startHealth;
        startMovSpeed = movSpeed;
        agent.SetDestination(WayPoints.endPoint.position);
        agent.speed = startMovSpeed;
    }

    private void Update()
    {
        distFromNexus = WaveSpawner.GetPathRemainingDistance(agent);

        if (distFromNexus <= 0.5f)
        {
            LifeManager.lifeInstance.DamagePlayer(damageToNexus);
            Destroy(gameObject);
            return;
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

    public void Slow(int slowValue)
    {
        StartCoroutine(ApplySLow(slowValue));
    }
    IEnumerator ApplySLow(int slowForce)
    {
        rend.material = slowMaterial;
        agent.speed = movSpeed - slowForce;
        yield return new WaitForSeconds(slowTime);
        movSpeed = startMovSpeed;
        rend.material = startMaterial;
    }


    public void Poison(int poisonDamage)
    {
        if (!isPoison)
        {
            StartCoroutine(AppalyPoison(poisonDamage));
        }
    }
    IEnumerator AppalyPoison(int poisonDamage)
    {
        isPoison = true;
        rend.material = poisionMaterial;
        poisonParticule.SetActive(true);
        int startPoisionDuration = poisonDuration;

        for (int i = 0; i < startPoisionDuration; i++)
        {
            TakeDamage(poisonDamage);
            yield return new WaitForSeconds(0.5f);
        }
        rend.material = startMaterial;
        poisonParticule.SetActive(false);
        isPoison = false;
    }
    
    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
