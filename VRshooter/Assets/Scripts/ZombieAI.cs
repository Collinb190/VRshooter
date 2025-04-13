using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Animator animator;
    private ZombieAnimationController animationController;
    internal NewZombieAI.ZombieState currentState;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;
    private float lastAttackTime = -999f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<ZombieAnimationController>();

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

        if (animationController == null)
        {
            Debug.LogError($"? {gameObject.name} is missing the ZombieAnimationController script!");
        }
    }

    private void Start()
    {
        AssignTarget();
        InvokeRepeating(nameof(CheckForMissingComponents), 1f, 1f); // ?? Runs every 1 second
    }

    private void Update()
    {
        if (agent == null || !agent.isOnNavMesh || target == null || animationController == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= animationController.attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            // Randomize between left and right attack
            int attackChoice = Random.Range(0, 2);
            if (attackChoice == 0)
                animationController.StartAttackLeft();
            else
                animationController.StartAttackRight();

            lastAttackTime = Time.time;
        }
        else
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(target.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(target.position);
            }
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
