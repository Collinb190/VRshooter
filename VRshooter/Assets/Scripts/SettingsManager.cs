using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Gameplay Settings")]
    public TMP_Dropdown difficultyDropdown;
    public Slider sensitivitySlider;

    [Header("Control Settings")]
    public TMP_Dropdown movementTypeDropdown;
    public TMP_Dropdown turnTypeDropDown;

    [Header("Audio Settings")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    void Start()
    {
        LoadSettings();

        // Gameplay Listeners
        difficultyDropdown.onValueChanged.AddListener(UpdateDifficulty);
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);

        // Control Listeners
        movementTypeDropdown.onValueChanged.AddListener(UpdateMovementType);
        turnTypeDropDown.onValueChanged.AddListener(UpdateTurnType);

        // Audio Listeners
        masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
    }

    public void LoadSettings()
    {
        difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty", 1);  // Default: Normal
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1.0f);  // Default: 1.0f

        movementTypeDropdown.value = PlayerPrefs.GetInt("MovementType", 0);  // Default: Teleport
        turnTypeDropDown.value = PlayerPrefs.GetInt("TurnType", 0);  // Default: Snap Turn

        // Audio Settings
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);  // Default: 1.0f
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);  // Default: 1.0f
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);  // Default: 1.0f
    }

    public void UpdateDifficulty(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value);
        PlayerPrefs.Save();
    }

    public void UpdateSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save();
    }

    public void UpdateMovementType(int value)
    {
        PlayerPrefs.SetInt("MovementType", value);
        PlayerPrefs.Save();
    }

    public void UpdateTurnType(int value)
    {
        PlayerPrefs.SetInt("TurnType", value);
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
}
