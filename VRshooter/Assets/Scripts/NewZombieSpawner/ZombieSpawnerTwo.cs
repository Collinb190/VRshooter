using UnityEngine;

public class ZombieSpawnerTwo : MonoBehaviour
{
    [Header("Zombie Setup")]
    public GameObject[] zombiePrefabs;      // Prefabs should have visuals disabled by default
    public int numberOfZombies = 10;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;         // Assign these in the Inspector

    void Start()
    {
        SpawnZombies();
    }

    void SpawnZombies()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            GameObject zombiePrefab = GetRandomZombie();
            Transform spawnPoint = GetRandomSpawnPoint();

            if (zombiePrefab != null && spawnPoint != null)
            {
                GameObject spawnedZombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

                // ?? Ensure the entire zombie is visible
                EnableAllVisuals(spawnedZombie);
            }
        }
    }

    void EnableAllVisuals(GameObject zombie)
    {
        // ?? Ensure everything inside this zombie is active (visuals, colliders, etc.)
        zombie.SetActive(true);

        foreach (Transform child in zombie.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true); // Recursively activate all children
        }
    }

    GameObject GetRandomZombie()
    {
        if (zombiePrefabs.Length == 0) return null;
        int index = Random.Range(0, zombiePrefabs.Length);
        return zombiePrefabs[index];
    }

    Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0) return null;
        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }
}
