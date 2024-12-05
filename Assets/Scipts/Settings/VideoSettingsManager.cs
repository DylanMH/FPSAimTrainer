using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsManager : MonoBehaviour
{
    [Header("Video Settings UI")]
    public TMP_Dropdown resolutionDropdown; // Dropdown for resolutions
    public Toggle fullscreenToggle; // Toggle for fullscreen
    public TMP_Dropdown qualityDropdown; // Dropdown for quality levels
    public TMP_Dropdown frameRateDropdown; // Dropdown for frame rate limits

    private Resolution[] availableResolutions;

    void Start()
    {
        // Initialize resolutions
        InitializeResolutions();

        // Initialize fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Initialize quality dropdown
        InitializeQualityDropdown();

        // Initialize frame rate dropdown
        InitializeFrameRateDropdown();
    }

    public void InitializeResolutions()
    {
        // Fetch available resolutions
        availableResolutions = Screen.resolutions;

        if (availableResolutions == null || availableResolutions.Length == 0)
        {
            Debug.LogError("No available resolutions found!");
            return;
        }

        // Clear the dropdown options
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> resolutionOptions = new List<string>();

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            Resolution res = availableResolutions[i];
            string resolutionOption = $"{res.width} x {res.height} @ {res.refreshRateRatio}Hz";
            resolutionOptions.Add(resolutionOption);

            if (
                res.width == Screen.currentResolution.width
                && res.height == Screen.currentResolution.height
            )
            {
                currentResolutionIndex = i;
            }
        }

        // Populate the dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Add listener for changes
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        Debug.Log("Resolution dropdown initialized successfully");
    }

    private void SetResolution(int index)
    {
        Resolution selectedResolution = availableResolutions[index];
        Screen.SetResolution(
            selectedResolution.width,
            selectedResolution.height,
            Screen.fullScreen
        );
        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void InitializeQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        string[] qualityLevels = QualitySettings.names;
        foreach (string quality in qualityLevels)
        {
            qualityDropdown.options.Add(new TMP_Dropdown.OptionData(quality));
        }

        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    private void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("QualityLevel", index);
        PlayerPrefs.Save();
    }

    private void InitializeFrameRateDropdown()
    {
        frameRateDropdown.ClearOptions();
        string[] frameRateOptions = { "30 FPS", "60 FPS", "120 FPS", "Unlimited" };
        foreach (string option in frameRateOptions)
        {
            frameRateDropdown.options.Add(new TMP_Dropdown.OptionData(option));
        }

        int savedIndex = PlayerPrefs.GetInt("FrameRateLimit", 3);
        frameRateDropdown.value = savedIndex;
        frameRateDropdown.RefreshShownValue();
        frameRateDropdown.onValueChanged.AddListener(SetFrameRateLimit);
    }

    private void SetFrameRateLimit(int index)
    {
        int[] frameRates = { 30, 60, 120, -1 }; // -1 means no limit
        Application.targetFrameRate = frameRates[index];
        PlayerPrefs.SetInt("FrameRateLimit", index);
        PlayerPrefs.Save();
    }

    private void LoadSavedSettings()
    {
        // Load saved resolution
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = savedResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            SetResolution(savedResolutionIndex);
        }

        // Load fullscreen state
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }

        // Load quality level
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            int savedQualityLevel = PlayerPrefs.GetInt("QualityLevel");
            qualityDropdown.value = savedQualityLevel;
            qualityDropdown.RefreshShownValue();
            QualitySettings.SetQualityLevel(savedQualityLevel);
        }

        // Load frame rate limit
        if (PlayerPrefs.HasKey("FrameRateLimit"))
        {
            int savedFrameRateIndex = PlayerPrefs.GetInt("FrameRateLimit");
            frameRateDropdown.value = savedFrameRateIndex;
            frameRateDropdown.RefreshShownValue();
            SetFrameRateLimit(savedFrameRateIndex);
        }
    }
}
