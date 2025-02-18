using UnityEngine;
using UnityEngine.AI;

public class ZombieClickMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask groundLayer;
    public LayerMask attackableLayer;
    public float attackRange = 2f;
    public float doubleClickTime = 0.3f;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public ZombieAnimationController animationController; // ?? **Fix: Handle attack properly**

    private Transform target;
    private float lastClickTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            agent.speed = walkSpeed;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackableLayer))
            {
                target = hit.transform;
                MoveToTarget(timeSinceLastClick <= doubleClickTime);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                target = null;
                MoveToPosition(hit.point, timeSinceLastClick <= doubleClickTime);
            }

            lastClickTime = Time.time;
        }
    }

    void MoveToPosition(Vector3 position, bool run)
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(position);
            isRunning = run;
            agent.speed = run ? runSpeed : walkSpeed;
        }
    }

    void MoveToTarget(bool run)
    {
        if (target != null && agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            isRunning = run;
            agent.speed = run ? runSpeed : walkSpeed;

            // ?? **Fix: Prevent movement after reaching target & attack instead**
            if (agent.remainingDistance <= attackRange)
            {
                agent.isStopped = true;
                animationController.StartAttack();
            }
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
