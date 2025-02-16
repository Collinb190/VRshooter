using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "GameScene";  // Set in Inspector
    [SerializeField] private string settingsSceneName = "SettingsScene";
    [SerializeField] private string creditsSceneName = "CreditsScene";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(settingsSceneName);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
