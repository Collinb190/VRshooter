using UnityEngine;

public class LinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;  // Zombie prefab to spawn
    [SerializeField] private Transform[] spawnPoints;  // Array of spawn points in the scene
    [SerializeField] private float spawnInterval = 5f; // Time interval between spawns

    private void Start()
    {
        InvokeRepeating(nameof(SpawnZombiesAtAllPoints), 0f, spawnInterval);  // Start spawning zombies at intervals
    }

    private void SpawnZombiesAtAllPoints()
    {
        if (spawnPoints.Length == 0) return; // If no spawn points are set, do nothing

        // Iterate over all spawn points and spawn a zombie at each one
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
