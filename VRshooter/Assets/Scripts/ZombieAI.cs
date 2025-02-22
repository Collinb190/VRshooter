using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class ZombieAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player").transform; // Fallback if no target is set
        }
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    // Trigger detection to trigger "Game Over" (when zombie gets too close to the player)
    private void OnTriggerEnter(Collider other)
    {
        // If the zombie touches the player, it's game over
        if (other.CompareTag("Player"))
        {
            // You can show a game over screen or just reload the scene
            Debug.Log("Game Over! You got bit!");
            //GameOver();
        }
    }

    // Method to handle game over
    private void GameOver()
    {
        // Reload the current scene (or load a game over screen)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
