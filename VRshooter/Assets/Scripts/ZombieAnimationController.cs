using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimationController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;

    [Header("Animation Speeds")]
    public float walkAnimationSpeed = 1.0f;
    public float runAnimationSpeed = 1.5f;
    public float attackAnimationSpeed = 1.2f;

    public float attackRange = 2f;
    private bool isAttacking = false;

    void Start()
    {
        // ✅ Make sure the Animator and NavMeshAgent are assigned
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            Debug.LogError("🚨 ERROR: Animator component is missing on " + gameObject.name);
        else
            Debug.Log("✅ Animator found on " + gameObject.name);

        if (agent == null)
            Debug.LogError("🚨 ERROR: NavMeshAgent component is missing on " + gameObject.name);
        else
            Debug.Log("✅ NavMeshAgent found on " + gameObject.name);

        animator.applyRootMotion = false;
    }

    void Update()
    {
        if (agent == null || animator == null) return;

        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        float speed = agent.velocity.magnitude;

        Debug.Log($"🚀 Speed: {speed}, Velocity: {agent.velocity}, Local Velocity: {localVelocity}");

        // ✅ Reset all movement booleans first
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunningFront", false);
        animator.SetBool("IsRunningBack", false);
        animator.SetBool("IsRunningLeft", false);
        animator.SetBool("IsRunningRight", false);

        // ✅ Set correct state based on movement
        if (speed > 3f) // Running animations
        {
            if (localVelocity.z > 0.5f)
            {
                animator.SetBool("IsRunningFront", true);
                Debug.Log("✅ Running Forward Triggered");
            }
            else if (localVelocity.z < -0.5f)
            {
                animator.SetBool("IsRunningBack", true);
                Debug.Log("✅ Running Back Triggered");
            }
            else if (localVelocity.x < -0.5f)
            {
                animator.SetBool("IsRunningLeft", true);
                Debug.Log("✅ Running Left Triggered");
            }
            else if (localVelocity.x > 0.5f)
            {
                animator.SetBool("IsRunningRight", true);
                Debug.Log("✅ Running Right Triggered");
            }

            animator.speed = runAnimationSpeed;
        }
        else if (speed > 0.1f) // Walking animation
        {
            animator.SetBool("IsWalking", true);
            Debug.Log("✅ Walking Triggered");
            animator.speed = walkAnimationSpeed;
        }
        else // Idle state
        {
            animator.SetBool("IsIdle", true);
            Debug.Log("✅ Idle Triggered");
            animator.speed = 1.0f;
        }

        // ✅ Debug: Check which parameters are set in the Animator
        Debug.Log($"Animator State: IsIdle={animator.GetBool("IsIdle")}, IsWalking={animator.GetBool("IsWalking")}, " +
                  $"IsRunningFront={animator.GetBool("IsRunningFront")}, IsRunningBack={animator.GetBool("IsRunningBack")}, " +
                  $"IsRunningLeft={animator.GetBool("IsRunningLeft")}, IsRunningRight={animator.GetBool("IsRunningRight")}");
    }

    public void StartAttackLeft()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("AttackLeft"); // ✅ Matches Animator Parameter
            agent.isStopped = true;
            animator.speed = attackAnimationSpeed;
            Debug.Log("✅ Left Attack Triggered");
        }
    }

    public void StartAttackRight()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("AttackRight"); // ✅ Matches Animator Parameter
            agent.isStopped = true;
            animator.speed = attackAnimationSpeed;
            Debug.Log("✅ Right Attack Triggered");
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
        agent.isStopped = false;
        animator.speed = 1.0f;
        Debug.Log("✅ Stopping Attack");
    }
}
