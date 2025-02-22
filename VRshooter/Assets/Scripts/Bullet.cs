using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
        else if (!other.CompareTag("Player") || !other.CompareTag("Weapon"))
        {
            Destroy(gameObject); // Destroy the bullet if it hits something other than the player or enemy
        }
    }
}