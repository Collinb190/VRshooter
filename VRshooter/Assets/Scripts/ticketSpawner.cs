using UnityEngine;

public class ticketSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnTicket();
    }

    private void SpawnTicket()
    {
        if (spawnPoints.Length == 0) return; // If no spawn points are set, do nothing

        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the zombie at the random spawn point
        Instantiate(ticketPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
