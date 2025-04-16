using UnityEngine;

public class ShootPistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    public float bulletSpeed = 10f;

    public AudioSource audioSource;
    public AudioClip[] gunshotClips;

    private int lastClipIndex = -1;

    public void Shoot()
    {
        Debug.Log("Gun Fired!");

        // Spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = gunBarrel.forward * bulletSpeed;

        // Play gunshot sound
        PlayGunshotSound();
    }

    void PlayGunshotSound()
    {
        if (gunshotClips.Length == 0 || audioSource == null) return;

        int index;
        do
        {
            index = Random.Range(0, gunshotClips.Length);
        } while (index == lastClipIndex && gunshotClips.Length > 1);

        lastClipIndex = index;
        audioSource.clip = gunshotClips[index];
        audioSource.Play();
    }
}
