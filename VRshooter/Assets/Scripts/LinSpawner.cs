using UnityEngine;

public class LinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] zombiePrefabs;    // Array of zombie prefabs
    [SerializeField] private Transform[] spawnPoints;       // Array of spawn points in the scene
    [SerializeField] private float spawnInterval = 3f;      // Time interval between spawns
    [SerializeField] private int maxEnemies = 30;           // Max number of enemies allowed at once
    [SerializeField] private int resumeSpawnThreshold = 15; // Resume spawning when enemies drop below this

    private bool spawningEnabled = true;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnZombiesAtAllPoints), 0f, spawnInterval);
    }

    private void Update()
    {
        int debugEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (debugEnemyCount > 45) Debug.Log(debugEnemyCount);
        if (debugEnemyCount < 15) Debug.Log(debugEnemyCount);
    }

    private void SpawnZombiesAtAllPoints()
    {
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount >= maxEnemies)
        {
            spawningEnabled = false;
            return;
        }
        else if (currentEnemyCount < resumeSpawnThreshold)
        {
            spawningEnabled = true;
        }

        if (!spawningEnabled || spawnPoints.Length == 0 || zombiePrefabs.Length == 0)
            return;

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject randomZombie = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            Instantiate(randomZombie, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
