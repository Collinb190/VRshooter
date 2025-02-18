using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimationController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public ZombieClickMove clickMoveScript;

    public float idleThreshold = 0.1f;
    public float walkThreshold = 2.5f;
    public float stoppingDistanceThreshold = 0.2f;

    private string currentTrigger = "";
    private bool isAttacking = false;

    void Start()
    {
        if (animator == null || agent == null || clickMoveScript == null)
            return;

        animator.applyRootMotion = false;
        SetAnimationTrigger("DoIdle"); // Start in idle
    }

    void Update()
    {
        if (animator == null || agent == null || clickMoveScript == null)
            return;

        // ?? **Fix: Prevent movement while attacking**
        if (isAttacking) return;

        // ?? **Fix: Ensure agent stops properly when reaching destination**
        if (!agent.pathPending && agent.remainingDistance <= stoppingDistanceThreshold)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            if (currentTrigger != "DoIdle")
                SetAnimationTrigger("DoIdle");

            return;
        }
        else
        {
            agent.isStopped = false;
        }

        // ?? **Movement Logic**
        float speed = agent.velocity.magnitude;
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);

        if (speed < idleThreshold)
        {
            SetAnimationTrigger("DoIdle");
        }
        else if (speed <= walkThreshold && !clickMoveScript.IsRunning())
        {
            SetAnimationTrigger("DoWalk");
        }
        else
        {
            float angle = Mathf.Atan2(localVelocity.x, localVelocity.z) * Mathf.Rad2Deg;

            if (angle > -45f && angle <= 45f)
            {
                SetAnimationTrigger("DoRunFront");
            }
            else if (angle > 45f && angle <= 135f)
            {
                SetAnimationTrigger("DoRunRight");
            }
            else if (angle < -45f && angle >= -135f)
            {
                SetAnimationTrigger("DoRunLeft");
            }
            else
            {
                SetAnimationTrigger("DoRunBack");
            }
        }
    }

    private void SetAnimationTrigger(string trigger)
    {
        if (currentTrigger == trigger)
            return;

        if (!string.IsNullOrEmpty(currentTrigger))
        {
            animator.ResetTrigger(currentTrigger);
        }

        animator.SetTrigger(trigger);
        currentTrigger = trigger;
    }

    // ?? **Fix: Handle Attack Animation**
    public void StartAttack()
    {
        isAttacking = true;
        SetAnimationTrigger("DoAttack");
        agent.isStopped = true;  // Prevent movement during attack
    }

    public void StopAttack()
    {
        isAttacking = false;
        agent.isStopped = false; // Allow movement after attack
        SetAnimationTrigger("DoIdle"); // Ensure animation resets
    }
}
