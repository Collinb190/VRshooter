using UnityEngine;
using UnityEngine.AI;

public class LinUndeadAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float movementThreshold = 0.1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            Debug.LogError("No Animator component found!");

        if (agent == null)
            Debug.LogError("No NavMeshAgent component found!");
    }

    private void Update()
    {
        if (agent == null || animator == null) return;

        // Check the agent’s current velocity
        bool isMoving = agent.velocity.magnitude > movementThreshold;
        animator.SetBool("run", isMoving);
    }
}

