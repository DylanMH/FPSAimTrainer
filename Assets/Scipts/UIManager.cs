using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels; // array of panels to manage

    private int activePanelIndex = 0; // index of the currently active panel
    private GameManager gameManager; // reference to the GameManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the GameManager instance
        gameManager = GameManager.Instance;

        // show the main menu panel by default
        ShowPanel(0);
    }

    public void ShowPanel(int panelIndex)
    {
        // show main menu panel by default and hide all other panels
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == panelIndex);
        }
    }
}
