using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

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
            State = GameState.Paused;
            //pausePanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
        else if (State == GameState.Paused)
        {
            State = GameState.Playing;
            //pausePanel.SetActive(false);
            Time.timeScale = 1f; // Resume the game
        }
    }
}

