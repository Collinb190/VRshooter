using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f); // Destroy bullet after 10 seconds
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checker"))
        {
            // Ignore collision with objects tagged as "Checker"
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Weapon"))
        {
            Destroy(gameObject); // Destroy the bullet if it hits something other than the player or enemy
        }
    }
}