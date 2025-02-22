using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;  // Zombie prefab to spawn
    [SerializeField] private Transform[] spawnPoints;  // Array of spawn points in the scene
    [SerializeField] private float spawnInterval = 5f; // Time interval between spawns

    private void Start()
    {
        InvokeRepeating(nameof(SpawnZombie), 0f, spawnInterval);  // Start spawning zombies at intervals
    }

    private void SpawnZombie()
    {
        if (spawnPoints.Length == 0) return; // If no spawn points are set, do nothing

        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the zombie at the random spawn point
        Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
