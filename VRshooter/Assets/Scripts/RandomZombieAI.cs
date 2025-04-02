using UnityEngine;
using UnityEngine.AI;

public class RandomZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("? NavMeshAgent is missing on " + gameObject.name);
            return;
        }

        // ? Ensure the agent is on the NavMesh before setting a destination
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("? " + gameObject.name + " is NOT on a NavMesh!");
            return;
        }

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
        if (agent == null || !agent.isOnNavMesh || target == null) return;

        agent.SetDestination(target.position);
    }
}
