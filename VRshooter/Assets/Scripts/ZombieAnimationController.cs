using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    [Header("Animation Speeds")]
    public float walkAnimationSpeed = 1.0f;
    public float runAnimationSpeed = 1.5f;
    public float attackAnimationSpeed = 1.2f;

    public float attackRange = 2f;
    private bool isAttacking = false;

    void Start()
    {
        // ? Ensure Animator and NavMeshAgent are always assigned
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            Debug.LogError("? ERROR: Animator component is missing on " + gameObject.name);
        if (agent == null)
            Debug.LogError("? ERROR: NavMeshAgent component is missing on " + gameObject.name);

        // ? Disable Root Motion to avoid animation controlling movement
        if (animator != null)
        {
            animator.applyRootMotion = false;
        }
    }

    void Update()
    {
        if (agent == null || animator == null) return;

        // ? Ensure movement animation plays properly
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        float speed = agent.velocity.magnitude;
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);

        // ? Reset movement states only when necessary (reduces animation bugs)
        bool isIdle = speed <= 0.1f;
        bool isWalking = speed > 0.1f && speed <= 2.5f;
        bool isRunning = speed > 2f;

        animator.SetBool("IsIdle", isIdle);
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunningFront", isRunning && localVelocity.z > 0.5f);
        animator.SetBool("IsRunningBack", isRunning && localVelocity.z < -0.5f);
        animator.SetBool("IsRunningLeft", isRunning && localVelocity.x < -0.5f);
        animator.SetBool("IsRunningRight", isRunning && localVelocity.x > 0.5f);

        // ? Adjust animation speed dynamically
        if (isRunning)
            animator.speed = runAnimationSpeed;
        else if (isWalking)
            animator.speed = walkAnimationSpeed;
        else
            animator.speed = 1.0f; // Default for idle
    }

    public void StartAttackLeft()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("AttackLeft"); // ? Matches Animator Parameter
            agent.isStopped = true; // ? Stops movement during attack
            animator.speed = attackAnimationSpeed;
            Invoke(nameof(ResumeMovement), 1.2f); // ? Resumes movement after attack
        }
    }

    public void StartAttackRight()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("AttackRight"); // ? Matches Animator Parameter
            agent.isStopped = true;
            animator.speed = attackAnimationSpeed;
            Invoke(nameof(ResumeMovement), 1.2f);
        }
    }

    private void ResumeMovement()
    {
        isAttacking = false;
        agent.isStopped = false;

        // ? Added pause before resuming movement to avoid animation bug
        StartCoroutine(ResetAnimatorSpeed());
    }

    private IEnumerator ResetAnimatorSpeed()
    {
        yield return new WaitForSeconds(0.2f); // Added this delay to help let attack finish.
        animator.speed = 1.0f;
    }
}
