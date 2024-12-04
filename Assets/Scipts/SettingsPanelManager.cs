using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanelManager : MonoBehaviour
{
    [Header("Settings Panels")]
    public GameObject settingsMenuPanel; // Main settings menu panel with Game, Video, Mouse, Audio buttons
    public GameObject gameSettingsPanel;
    public GameObject videoSettingsPanel;
    public GameObject mouseSettingsPanel;
    public GameObject audioSettingsPanel;

    private GameObject previousPanel; // Track the previous panel for back button logic

    void Start()
    {
        ShowSettingsMenu(); // Show main settings menu by default
    }

    // Show the main settings menu
    public void ShowSettingsMenu()
    {
        DisableAllPanels();
        settingsMenuPanel.SetActive(true);
        previousPanel = null; // Reset previous panel
    }

    // Show Game settings panel
    public void ShowGameSettings()
    {
        DisableAllPanels();
        gameSettingsPanel.SetActive(true);
        previousPanel = settingsMenuPanel; // Remember to go back to the main settings menu
    }

    // Show Video settings panel
    public void ShowVideoSettings()
    {
        DisableAllPanels();
        videoSettingsPanel.SetActive(true);
        previousPanel = settingsMenuPanel;
    }

    // Show Mouse settings panel
    public void ShowMouseSettings()
    {
        DisableAllPanels();
        mouseSettingsPanel.SetActive(true);
        previousPanel = settingsMenuPanel;
    }

    // Show Audio settings panel
    public void ShowAudioSettings()
    {
        DisableAllPanels();
        audioSettingsPanel.SetActive(true);
        previousPanel = settingsMenuPanel;
    }

    // Handle the back button logic
    public void OnBackButton()
    {
        if (previousPanel != null)
        {
            DisableAllPanels();
            previousPanel.SetActive(true);
            previousPanel = null; // Clear the previous panel after returning
        }
        else
        {
            // If no previous panel, determine which main menu to return to based on the scene
            string currentScene = GameManager.Instance.currentScene;

            if (currentScene == "MainMenu")
            {
                FindFirstObjectByType<MainMenuUIManager>().ShowMainMenuPanel(); // Return to the main menu
            }
            else if (currentScene == "GameScene")
            {
                FindFirstObjectByType<GameMenuUIManager>().ShowGameMenu(); // Return to the game menu
            }
        }
    }

    // Helper to disable all panels
    private void DisableAllPanels()
    {
        settingsMenuPanel.SetActive(false);
        gameSettingsPanel.SetActive(false);
        videoSettingsPanel.SetActive(false);
        mouseSettingsPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
    }
}
