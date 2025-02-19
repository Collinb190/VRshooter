using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public enum ZombieState { Idle, Wandering, Chasing, Attacking }
    public ZombieState currentState = ZombieState.Wandering;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    [Header("Zombie Settings")]
    public float detectionRange = 15f; // Distance at which the zombie detects the player
    public float attackRange = 2f; // Distance to attack
    public float wanderRadius = 10f; // How far zombies can wander
    public float wanderTimer = 5f; // Time before choosing a new random spot
    public float alertRadius = 10f; // Range to alert other zombies
    public float zombieSpeed = 3.5f; // ✅ Adjustable zombie speed
    public LayerMask obstacleLayer;
    public LayerMask destructibleLayer;

    private float wanderTimerCountdown;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // ✅ Find the player at Start
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("🚨 ERROR: Player not found! Make sure the Player GameObject is tagged 'Player'.");
        }

        agent.speed = zombieSpeed;
        wanderTimerCountdown = wanderTimer;
        ChangeState(ZombieState.Wandering);
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("⚠️ No player detected, zombie is wandering.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log($"🔍 Distance to Player: {distanceToPlayer}");

        if (distanceToPlayer <= attackRange)
        {
            ChangeState(ZombieState.Attacking);
        }
        else if (distanceToPlayer <= detectionRange)
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
            ChasePlayer(); // ✅ Continuously update chase destination
        }

        SyncAnimations();
    }

    void ChangeState(ZombieState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log($"🔄 Changing State: {newState}");

            switch (newState)
            {
                case ZombieState.Chasing:
                    agent.speed = zombieSpeed;
                    agent.isStopped = false;
                    Debug.Log("🚀 Chasing Player...");
                    break;

                case ZombieState.Attacking:
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        agent.isStopped = true;
                        animator.SetTrigger("Attack");
                        Debug.Log("⚔️ ATTACKING!");
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
        Debug.Log($"🚀 Updating Chase Target: {player.position}");
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
                Debug.Log($"🛑 Roaming to: {hit.position}");
            }
            wanderTimerCountdown = wanderTimer;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layerMask))
        {
            return navHit.position;
        }
        return origin;
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("🩸 Zombie Attacks Player!");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        ChangeState(ZombieState.Chasing);
    }

    void CheckForAlertedZombies()
    {
        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, alertRadius);
        foreach (Collider zombie in nearbyZombies)
        {
            if (zombie.CompareTag("Enemy") && zombie.GetComponent<ZombieAI>().currentState == ZombieState.Chasing)
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
        float speed = agent.velocity.magnitude;
        animator.SetBool("IsWalking", speed > 0.1f);
        animator.SetBool("IsRunningFront", speed > 3f);
        Debug.Log($"🌀 Speed: {speed} | Walking: {animator.GetBool("IsWalking")} | Running: {animator.GetBool("IsRunningFront")}");
    }
}
