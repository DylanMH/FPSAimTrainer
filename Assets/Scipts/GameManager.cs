using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public bool isGamePaused = false;
    public string currentScene = ""; // name of the current scene
    public string selectedTask = ""; // name of the currently selected task

    [Header("Game Data")]
    public int score = 0;

    private void Awake()
    {
        // ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // keep GameManager alive across scenes
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize any global systems or data here
        currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("GameManager is alive!");
    }

    public void LoadScene(string sceneName)
    {
        // Load a scene by name
        currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    public void SelectedTask(string taskName)
    {
        // Set the selected task
        selectedTask = taskName;
        Debug.Log("Selected task: " + selectedTask);
    }

    public void PauseGame()
    {
        // pause the game
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // resume the game
        isGamePaused = false;
        Time.timeScale = 1f;
    }
}
