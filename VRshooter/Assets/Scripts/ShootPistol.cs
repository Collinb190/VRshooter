using UnityEngine;

public class ShootPistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunBarrel; // The position from which the bullet is shot
    public float bulletSpeed = 10f;

    public void Shoot()
    {
        Debug.Log("Gun Fired!");
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = gunBarrel.forward * bulletSpeed; // Shoot in the forward direction of the gun
    }
}
