using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for coroutines

public class PlayerCollidesZombie : MonoBehaviour
{
    public GameObject deathCanvas; // Assign a UI win screen in the Inspector

    private void Start()
    {
        deathCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player has been bitten");
            Time.timeScale = 0f; // Freeze the game
            deathCanvas.SetActive(true);
            StartCoroutine(GameOver()); // Start the coroutine
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(3f); // Wait for 3 seconds (ignores Time.timeScale)
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
