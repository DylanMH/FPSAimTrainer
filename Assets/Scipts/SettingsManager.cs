using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("Player Settings")]
    public float mouseSensitivity = 1f;
    public bool isFullScreen = true;

    private void Awake()
    {
        // ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // keep SettingsManager alive across scenes
    }

    public void SaveSettings()
    {
        // Save the player settings
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        // Load the player settings
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
    }
}
