using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    public float walkSpeedThreshold = 1f;
    public float runSpeedThreshold = 3f;

    public float walkStepInterval = 0.6f;
    public float runStepInterval = 0.3f;

    private CharacterController characterController;
    private float stepTimer = 0f;
    private int lastClipIndex = -1;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!characterController.isGrounded || characterController.velocity.magnitude < 0.1f)
            return;

        float speed = characterController.velocity.magnitude;

        // Choose interval based on speed
        float interval = speed > runSpeedThreshold ? runStepInterval :
                         speed > walkSpeedThreshold ? walkStepInterval :
                         0f;

        if (interval > 0f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayRandomFootstep();
                stepTimer = interval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index;
        do
        {
            index = Random.Range(0, footstepClips.Length);
        } while (index == lastClipIndex && footstepClips.Length > 1); // Avoid repeat

        lastClipIndex = index;
        audioSource.clip = footstepClips[index];
        audioSource.Play();
    }
}
