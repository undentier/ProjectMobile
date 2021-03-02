using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header ("Stats")]
    public float movSpeed = 10f;
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

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startMaterial = rend.material;
        actualHealth = startHealth;
        startMovSpeed = movSpeed;
        agent.SetDestination(WayPoints.points.position);
    }

    private void Update()
    {

        if (gameObject.transform.position.z == WayPoints.points.position.z)
        {
            LifeManager.lifeInstance.DamagePlayer(damageToNexus);
            Destroy(gameObject);
            return;
        }

        if (agent.pathPending)
        {
            agent.speed = movSpeed;
        }
        distFromNexus = agent.remainingDistance;
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
        agent.speed = startMovSpeed;
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
