using UnityEngine;

public class ZombieSpawnerNow : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // Array to store multiple zombie prefabs
    public int numberOfZombies = 5;
    public float spawnRadius = 20f;

    void Start()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            GameObject randomZombie = GetRandomZombiePrefab();
            Instantiate(randomZombie, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0; // Adjust for ground level
        return randomPos;
    }

    GameObject GetRandomZombiePrefab()
    {
        if (zombiePrefabs.Length == 0)
        {
            Debug.LogError("No zombie prefabs assigned!");
            return null;
        }
        int randomIndex = Random.Range(0, zombiePrefabs.Length);
        return zombiePrefabs[randomIndex];
    }
}
