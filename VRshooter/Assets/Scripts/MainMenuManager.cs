using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenu;  // Main menu GameObject
    public GameObject settingsMenu;  // Settings menu GameObject
    public GameObject creditsMenu;  // Credits menu GameObject
    public GameObject endCreditsMenu;  // End credits menu GameObject
    public GameObject endMenu;  // End menu GameObject
    public GameObject whiteWallStart;  // Starting White Wall
    public GameObject spawnManager;  // Credits menu GameObject

    void Start()
    {
        ShowMainMenu();  // Start with the main menu visible
    }

    public void StartGame()
    {
        // Here, you can load the game scene or start the game in the current scene
        Debug.Log("Game Starting...");
        whiteWallStart.SetActive(false);
        spawnManager.SetActive(true);
    }

    public void OpenSettings()
    {
        ShowSettingsMenu();  // Show the settings menu
    }

    public void OpenCredits()
    {
        ShowCreditsMenu();  // Show the credits menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Functions to show specific menus
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void ShowCreditsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ShowEndCreditsMenu()
    {
        endCreditsMenu.SetActive(true);
        endMenu.SetActive(false);
    }
    public void ShowEndMenu()
    {
        endCreditsMenu.SetActive(false);
        endMenu.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time is running again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}