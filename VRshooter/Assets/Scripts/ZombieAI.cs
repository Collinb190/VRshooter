using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Animator animator;
    internal NewZombieAI.ZombieState currentState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError($"? {gameObject.name} is missing a NavMeshAgent at spawn!");
            Destroy(gameObject);
            return;
        }

        if (animator == null)
        {
            Debug.LogError($"? {gameObject.name} is missing an Animator!");
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        AssignTarget();
        InvokeRepeating(nameof(CheckForMissingComponents), 1f, 1f); // ?? Runs every 1 second
    }

    private void Update()
    {
        if (agent != null && agent.isOnNavMesh && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void AssignTarget()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("? No GameObject with 'Player' tag found! Ensure XR Origin is tagged correctly.");
        }
    }

    private void CheckForMissingComponents()
    {
        if (animator == null)
        {
            Debug.LogError($"? {gameObject.name} LOST its Animator at runtime!");
            return;
        }

        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError($"? {gameObject.name} LOST its ZombieAnimationController at runtime! Reassigning...");

            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("ZombieAnimationController");

            if (animator.runtimeAnimatorController == null)
            {
                Debug.LogError($"❌ CRITICAL: Failed to reload ZombieAnimationController. Ensure it exists in Resources.");
            }
            else
            {
                Debug.Log($"✅ Successfully restored Animator Controller for {gameObject.name}.");
            }
        }
    }
}
