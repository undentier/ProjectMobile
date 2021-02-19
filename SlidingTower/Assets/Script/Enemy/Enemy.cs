using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header ("Stats")]
    public float movSpeed = 10f;
    public float startHealth;
    [HideInInspector]
    public float actualHealth;
    public int damageToNexus;

    [Header ("Unity Setup")]
    public NavMeshAgent agent;
    public GameObject deathEffect;
    public Image healhtBar;

    private void Start()
    {
        actualHealth = startHealth;
        agent.speed = movSpeed;
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
    }

    public void TakeDamage(int amount)
    {
        actualHealth -= amount;

        healhtBar.fillAmount = actualHealth / startHealth;

        if (actualHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
