using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "GameScene";  // Set in Inspector
    [SerializeField] private string settingsSceneName = "SettingsScene";
    [SerializeField] private string creditsSceneName = "CreditsScene";

    [Header("UI Elements")]
    [SerializeField] private TMP_Text statusText; // TMP Text for feedback

    public void StartGame()
    {
        statusText.text = "Loading Game...";
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        statusText.text = "Opening Settings...";
        SceneManager.LoadScene(settingsSceneName);
    }

    public void OpenCredits()
    {
        statusText.text = "Opening Credits...";
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        statusText.text = "Quitting Game...";
        Application.Quit();
    }
}