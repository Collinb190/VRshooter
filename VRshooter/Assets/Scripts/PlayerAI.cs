using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float roamRadius = 20f;
    public float roamDelay = 3f;
    public float moveSpeed = 5f;

    private float lastRoamTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("🚨 ERROR: No NavMeshAgent found on " + gameObject.name);
        }

        agent.speed = moveSpeed; // Ensure agent has speed
        RoamRandomly();
    }

    void Update()
    {
        if (Time.time - lastRoamTime > roamDelay)
        {
            RoamRandomly();
        }
    }

    void RoamRandomly()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log($"🎯 Player moving to: {hit.position}");
        }
        lastRoamTime = Time.time;
    }
}
