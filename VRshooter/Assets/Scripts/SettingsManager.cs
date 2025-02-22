using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Gameplay Settings")]
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button snapTurnButton;
    public Button smoothTurnButton;
    public Button leftHandButton;
    public Button rightHandButton;

    [Header("Audio Settings")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    public Button backButton; // Back Button
    public Button quitButton; // Quit Button

    void Start()
    {
        // Gameplay Settings
        easyButton.onClick.AddListener(() => SetDifficulty(0));
        normalButton.onClick.AddListener(() => SetDifficulty(1));
        hardButton.onClick.AddListener(() => SetDifficulty(2));

        snapTurnButton.onClick.AddListener(() => SetTurnType(0));
        smoothTurnButton.onClick.AddListener(() => SetTurnType(1));

        leftHandButton.onClick.AddListener(() => SetHandDominance(0));
        rightHandButton.onClick.AddListener(() => SetHandDominance(1));

        // Audio Settings
        masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);

        // Back & Quit Buttons
        backButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Load Settings
        LoadSettings();
    }

    public void SetDifficulty(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
        PlayerPrefs.Save();
    }

    public void SetTurnType(int value)
    {
        PlayerPrefs.SetInt("TurnType", value);
        PlayerPrefs.Save();
    }

    public void SetHandDominance(int value)
    {
        PlayerPrefs.SetInt("HandDominance", value);
        PlayerPrefs.Save();
    }

    public void UpdateMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    public void UpdateMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        easyButton.interactable = PlayerPrefs.GetInt("Difficulty", 1) == 0;
        normalButton.interactable = PlayerPrefs.GetInt("Difficulty", 1) == 1;
        hardButton.interactable = PlayerPrefs.GetInt("Difficulty", 1) == 2;

        snapTurnButton.interactable = PlayerPrefs.GetInt("TurnType", 0) == 0;
        smoothTurnButton.interactable = PlayerPrefs.GetInt("TurnType", 0) == 1;

        leftHandButton.interactable = PlayerPrefs.GetInt("HandDominance", 0) == 0;
        rightHandButton.interactable = PlayerPrefs.GetInt("HandDominance", 0) == 1;

        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    public void ReturnToMainMenu()
    {
        // Ensure the MenuManager is referenced correctly to show the main menu
        MenuManager menuManager = FindObjectOfType<MenuManager>();
        if (menuManager != null)
        {
            menuManager.ShowMainMenu(); // Switch to the main menu when back is pressed
        }
    }

    public void QuitGame()
    {
        Application.Quit();  // Quit the application
    }
}