using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenu;  // Main menu GameObject
    public GameObject settingsMenu;  // Settings menu GameObject
    public GameObject creditsMenu;  // Credits menu GameObject
    public GameObject whiteWall;  // Credits menu GameObject
    public GameObject spawnManager;  // Credits menu GameObject

    void Start()
    {
        ShowMainMenu();  // Start with the main menu visible
    }

    public void StartGame()
    {
        // Here, you can load the game scene or start the game in the current scene
        Debug.Log("Game Starting...");
        whiteWall.SetActive(false);
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
}