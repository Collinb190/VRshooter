using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NewZombieAI : MonoBehaviour
{
    public enum ZombieState { Idle, Wandering, Chasing, Attacking }
    public ZombieState currentState = ZombieState.Wandering;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    [Header("Zombie Settings")]
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float alertRadius = 10f;
    public float zombieSpeed = 3.5f;
    public float attackCooldown = 0.2f; // ? Added attack cooldown
    public LayerMask obstacleLayer;
    public LayerMask destructibleLayer;

    private float wanderTimerCountdown;
    private bool isAttacking = false;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError($"?? ERROR: {name} has NO Animator component! Fix this in the prefab.");
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("?? ERROR: Player not found! Make sure the Player GameObject is tagged 'Player'.");
        }

        agent.speed = zombieSpeed;
        wanderTimerCountdown = wanderTimer;
        ChangeState(ZombieState.Wandering);
    }

    void Update()
    {
        if (animator == null)
        {
            Debug.LogError($"? {name} LOST its Animator at runtime!");
            animator = GetComponent<Animator>(); // Try to recover
            return;
        }

        if (player == null)
        {
            Debug.LogWarning("? No player detected, zombie is wandering.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log($"?? Distance to Player: {distanceToPlayer}");

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime > attackCooldown)
        {
            ChangeState(ZombieState.Attacking);
        }
        else if (distanceToPlayer <= detectionRange && currentState != ZombieState.Attacking)
        {
            ChangeState(ZombieState.Chasing);
        }
        else if (currentState == ZombieState.Wandering)
        {
            Wander();
        }
        else if (currentState == ZombieState.Idle)
        {
            CheckForAlertedZombies();
        }

        if (currentState == ZombieState.Chasing)
        {
            ChasePlayer();
        }

        SyncAnimations();
    }

    void ChangeState(ZombieState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log($"?? Changing State: {newState}");

            switch (newState)
            {
                case ZombieState.Chasing:
                    agent.speed = zombieSpeed;
                    agent.isStopped = false;
                    Debug.Log("?? Chasing Player...");
                    break;

                case ZombieState.Attacking:
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        agent.isStopped = true;

                        // ? Randomly select Attack Left or Attack Right
                        if (UnityEngine.Random.value > 0.5f)
                        {
                            animator.SetTrigger("AttackLeft");
                            Debug.Log("? Left Attack Triggered!");
                        }
                        else
                        {
                            animator.SetTrigger("AttackRight");
                            Debug.Log("? Right Attack Triggered!");
                        }

                        lastAttackTime = Time.time; // ? Set attack cooldown timer
                        StartCoroutine(AttackPlayer());
                    }
                    break;

                case ZombieState.Wandering:
                    Wander();
                    break;
            }
        }
    }

    void ChasePlayer()
    {
        if (player == null || isAttacking) return;

        agent.SetDestination(player.position);
        Debug.Log($"?? Updating Chase Target: {player.position}");
    }

    void Wander()
    {
        if (!agent.isOnNavMesh) return;

        wanderTimerCountdown -= Time.deltaTime;
        if (wanderTimerCountdown <= 0)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            if (NavMesh.SamplePosition(newPos, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                Debug.Log($"?? Roaming to: {hit.position}");
            }
            wanderTimerCountdown = wanderTimer;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layerMask))
        {
            return navHit.position;
        }
        return origin;
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(0.1f); // Wait before dealing damage
        Debug.Log("?? Zombie Attacks Player!");

        yield return new WaitForSeconds(1.2f); // Ensure full attack animation plays
        isAttacking = false;
        ChangeState(ZombieState.Chasing); // ? Resume chasing after attacking
    }

    void CheckForAlertedZombies()
    {
        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, alertRadius);
        foreach (Collider zombie in nearbyZombies)
        {
            if (zombie.CompareTag("Enemy") && zombie.GetComponent<NewZombieAI>().currentState == ZombieState.Chasing)
            {
                ChangeState(ZombieState.Chasing);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & destructibleLayer) != 0)
        {
            ChangeState(ZombieState.Attacking);
            Destroy(other.gameObject, 2f);
        }
    }

    void SyncAnimations()
    {
        if (animator == null)
        {
            Debug.LogError($"{name} is missing an Animator at runtime!");
            animator = GetComponent<Animator>(); // Try to recover
            return;
        }

        float speed = agent.velocity.magnitude;
        animator.SetBool("IsWalking", speed > 0.1f);
        animator.SetBool("IsRunningFront", speed > 3f);
        Debug.Log($"?? Speed: {speed} | Walking: {animator.GetBool("IsWalking")} | Running: {animator.GetBool("IsRunningFront")}");
    }
}
