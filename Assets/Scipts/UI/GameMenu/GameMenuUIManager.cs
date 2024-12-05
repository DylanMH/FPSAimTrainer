using UnityEngine;

public class GameMenuUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject gameMenuPanel; // reference to the game menu panel
    public GameObject settingsPanel; // reference to the settings panel

    public void ShowGameMenu() => ShowPanel(gameMenuPanel);

    public void ShowSettings() => ShowPanel(settingsPanel);

    private void ShowPanel(GameObject panelToShow)
    {
        if (GameManager.Instance.isGamePaused == true)
        {
            gameMenuPanel.SetActive(panelToShow == gameMenuPanel);
            settingsPanel.SetActive(panelToShow == settingsPanel);
        }
    }
}
