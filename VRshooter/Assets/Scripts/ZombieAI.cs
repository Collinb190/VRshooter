using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player").transform; // Fallback if no target is set
        }
    }

    private void Update()
    {
        if (target != null)
        {
            NavMeshPath path = new NavMeshPath();

            // Check if there's a valid path to the player
            if (agent.CalculatePath(target.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(target.position);
            }
        }
    }
}
