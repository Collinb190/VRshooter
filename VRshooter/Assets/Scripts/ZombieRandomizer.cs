using UnityEngine;
using UnityEngine.AI;

public class ZombieRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] zombiePrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minSpeed = 0.8f;
    [SerializeField] private float maxSpeed = 2.0f;

    private void Start()
    {
        InvokeRepeating(nameof(RandomizedSpawnZombie), 0f, spawnInterval);
    }

    private void RandomizedSpawnZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefabs.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject zombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = Random.Range(minSpeed, maxSpeed);
        }

        ZombieAI ai = zombie.GetComponent<ZombieAI>();
        if (ai != null && !ai.enabled)
        {
            ai.enabled = true;
        }
    }
}
