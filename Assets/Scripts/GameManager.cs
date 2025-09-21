using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    [SerializeField] private Transform pausePanel;

    public int Score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Object remains after scene change.
        }
        else
        {
            Destroy(gameObject); // Destroy the new instance instead of the old one.
        }
    }

    public void HandleEscapeButton()
    {
        if (State == GameState.Playing)
        {
            PauseGame();
        }
        else if (State == GameState.Paused)
        {
            UnpauseGame();
        }
    }

    public void PauseGame()
    {
        State = GameState.Paused;
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void UnpauseGame()
    {
        State = GameState.Playing;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}

