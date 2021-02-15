using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header ("Stats")]
    public float movSpeed = 10f;

    [Header ("Unity Setup")]
    public NavMeshAgent agent;

    private void Start()
    {
        agent.speed = movSpeed;
        agent.SetDestination(WayPoints.points.position);
    }

    private void Update()
    {
        if (gameObject.transform.position.z == WayPoints.points.position.z)
        {
            Destroy(gameObject);
            return;
        }
    }
}
