using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    // panels to load settings from
    [Header("Settings Panels")]
    public GameObject gameSettingsPanel;
    public GameObject videoSettingsPanel;
    public GameObject mouseSettingsPanel;
    public GameObject audioSettingsPanel;

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

    private void Start()
    {
        gameSettingsPanel.GetComponent<GameSettingsManager>().LoadSavedSettings();
        videoSettingsPanel.GetComponent<VideoSettingsManager>().LoadSavedSettings();
        /*         mouseSettingsPanel.GetComponent<MouseSettingsManager>().LoadSavedSettings();
                audioSettingsPanel.GetComponent<AudioSettingsManager>().LoadSavedSettings(); */
    }
}
