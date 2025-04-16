using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endMenu; // Assign a UI win screen in the Inspector

    private void Start()
    {
        endMenu.SetActive(false); // Ensure end menue is not active at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ticket"))
        {
            Debug.Log("Player reached the end zone with ticket!");
            ActivateWinScreen();
        }
    }

    void ActivateWinScreen()
    {
        if (endMenu != null)
        {
            endMenu.SetActive(true);
        }
        Time.timeScale = 0f; // Freeze the game
    }
}
