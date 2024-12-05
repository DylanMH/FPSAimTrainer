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

    private void InitializeResolutions()
    {
        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string resolutionOption =
                $"{availableResolutions[i].width} x {availableResolutions[i].height} @ {availableResolutions[i].refreshRateRatio}Hz";
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionOption));

            if (
                availableResolutions[i].width == Screen.currentResolution.width
                && availableResolutions[i].height == Screen.currentResolution.height
            )
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
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

    public void LoadSavedSettings()
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
