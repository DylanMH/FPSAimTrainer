using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject taskSelectorPanel;

    void Start()
    {
        // Show the main menu panel by default
        ShowMainMenuPanel();
    }

    // Show Main Menu Panel
    public void ShowMainMenuPanel()
    {
        EnablePanel(mainMenuPanel);
        DisablePanel(settingsMenuPanel);
        DisablePanel(taskSelectorPanel);
    }

    // Show Settings Panel
    public void ShowSettingsPanel()
    {
        DisablePanel(mainMenuPanel);
        // access the SettingsPanelManager script to show the settings menu
        settingsMenuPanel.GetComponent<SettingsPanelManager>().ShowSettingsMenu();
    }

    // Show Task Selector Panel
    public void ShowTaskSelectorPanel()
    {
        EnablePanel(taskSelectorPanel);
        DisablePanel(mainMenuPanel);
        DisablePanel(settingsMenuPanel);
    }

    // Helper methods to enable/disable panels
    private void EnablePanel(GameObject panel)
    {
        if (panel != null)
            panel.SetActive(true);
    }

    private void DisablePanel(GameObject panel)
    {
        if (panel != null)
            panel.SetActive(false);
    }

    // Optional: Add a quit method for the Quit button
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
