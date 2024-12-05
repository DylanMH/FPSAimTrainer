using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseSettingsManager : MonoBehaviour
{
    [Header("Mouse Settings UI")]
    public TMP_Dropdown gameDropdown; // Dropdown for selecting the game
    public TMP_InputField sensitivityInput; // Input field for sensitivity
    public Toggle rawInputToggle; // Toggle for raw input
    public TMP_InputField fovInput; // Input field for FOV

    [Header("Mouse Settings")]
    public float currentSensitivity = 0.17f; // Default sensitivity
    public string selectedGame = "Valorant"; // Default game
    public bool rawInput = true; // Default raw input
    public float currentFOV = 103f; // Default FOV (Valorant FOV)

    private const float valorantSensitivityScaling = 0.07f;
    private const float cs2SensitivityScaling = 0.022f;

    private const float valorantFOV = 71.15f; // Valorant vertical FOV for 16:9
    private const float cs2FOV = 73.74f; // CS2 vertical FOV for 16:9

    private void Start()
    {
        InitializeGameDropdown();
        LoadSavedSettings();

        // Sync UI events
        sensitivityInput.onEndEdit.AddListener(OnSensitivityChanged);
        gameDropdown.onValueChanged.AddListener(OnGameChanged);
        rawInputToggle.onValueChanged.AddListener(OnRawInputToggled);
        fovInput.onEndEdit.AddListener(OnFOVChanged);
    }

    private void InitializeGameDropdown()
    {
        gameDropdown.ClearOptions();
        gameDropdown.AddOptions(new System.Collections.Generic.List<string> { "Valorant", "CS2" });
        gameDropdown.value = selectedGame == "Valorant" ? 0 : 1;
        gameDropdown.RefreshShownValue();
    }

    private void OnSensitivityChanged(string value)
    {
        if (float.TryParse(value, out float newSensitivity))
        {
            currentSensitivity = newSensitivity;
            PlayerPrefs.SetFloat("MouseSensitivity", currentSensitivity);
            PlayerPrefs.Save();
            UpdateMouseSensitivity();
        }
    }

    private void OnGameChanged(int index)
    {
        selectedGame = index == 0 ? "Valorant" : "CS2";
        PlayerPrefs.SetString("SelectedGame", selectedGame);
        PlayerPrefs.Save();
        UpdateMouseSensitivity();
        UpdateFOV();
    }

    private void OnRawInputToggled(bool isRaw)
    {
        rawInput = isRaw;
        PlayerPrefs.SetInt("RawInput", rawInput ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnFOVChanged(string value)
    {
        if (float.TryParse(value, out float newFOV))
        {
            currentFOV = Mathf.Clamp(newFOV, 60f, 120f); // Clamp FOV to reasonable limits
            PlayerPrefs.SetFloat("FOV", currentFOV);
            PlayerPrefs.Save();
            UpdateFOV();
        }
    }

    private void UpdateMouseSensitivity()
    {
        // Adjust sensitivity scaling based on the selected game
        float scalingFactor =
            selectedGame == "Valorant" ? valorantSensitivityScaling : cs2SensitivityScaling;
        float adjustedSensitivity = currentSensitivity * scalingFactor;

        // Assuming a player controller handles look speed, apply the sensitivity here
        PlayerController playerController = FindFirstObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerController.mouseSensitivity = adjustedSensitivity;
        }
    }

    private void UpdateFOV()
    {
        // Adjust FOV based on the selected game
        currentFOV = selectedGame == "Valorant" ? valorantFOV : cs2FOV;

        // Optionally override with custom FOV if set in the UI
        if (float.TryParse(fovInput.text, out float customFOV))
        {
            currentFOV = Mathf.Clamp(customFOV, 60f, 120f);
        }

        // Assuming a camera manager or similar script exists
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = currentFOV;
        }

        PlayerPrefs.SetFloat("FOV", currentFOV);
        PlayerPrefs.Save();
    }

    private void LoadSavedSettings()
    {
        // Load saved sensitivity
        currentSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.17f);
        sensitivityInput.text = currentSensitivity.ToString("F2");

        // Load saved game
        selectedGame = PlayerPrefs.GetString("SelectedGame", "Valorant");
        gameDropdown.value = selectedGame == "Valorant" ? 0 : 1;

        // Load raw input setting
        rawInput = PlayerPrefs.GetInt("RawInput", 1) == 1;
        rawInputToggle.isOn = rawInput;

        // Load FOV
        currentFOV = PlayerPrefs.GetFloat("FOV", 103f);
        fovInput.text = currentFOV.ToString("F1");

        // Apply saved settings
        UpdateMouseSensitivity();
        UpdateFOV();
    }
}
