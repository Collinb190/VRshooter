using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject endMenu; // Assign a UI win screen in the Inspector
    public GameObject endCreditMenu; // Assign a UI win screen in the Inspector
    public GameObject whiteWallEnd;  // Ending White Wall

    private void Start()
    {
        whiteWallEnd.SetActive(false); // Ensure wall is not active at start
        endMenu.SetActive(false); // Ensure end menue is not active at start
        endCreditMenu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            Debug.Log("Player reached the end zone!");
            whiteWallEnd.SetActive(true);
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
