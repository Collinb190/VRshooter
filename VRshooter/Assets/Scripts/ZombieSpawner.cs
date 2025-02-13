using UnityEngine;
public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int numberOfZombies = 5;
    public float spawnRadius = 20f;
    void Start()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        }
    }
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0; // Adjust for ground level
        return randomPos;
    }
}
