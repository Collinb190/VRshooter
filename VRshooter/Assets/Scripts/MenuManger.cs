using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    void Start()
    {
        ShowMainMenu();  // Starts with main menu visible
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}