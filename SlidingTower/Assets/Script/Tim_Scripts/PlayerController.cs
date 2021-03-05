using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        agent.SetDestination(WayPoints.endPoint.position);
    }

}
