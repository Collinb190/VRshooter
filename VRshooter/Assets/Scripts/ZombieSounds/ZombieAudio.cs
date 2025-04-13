using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public ZombieSoundLibrary soundLibrary;
    private AudioSource audioSource;

    private float minDelay = 2f;
    private float maxDelay = 6f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayZombieSounds());
    }

    System.Collections.IEnumerator PlayZombieSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (!audioSource.isPlaying && soundLibrary.zombieSounds.Length > 0)
            {
                AudioClip clip = soundLibrary.zombieSounds[Random.Range(0, soundLibrary.zombieSounds.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
