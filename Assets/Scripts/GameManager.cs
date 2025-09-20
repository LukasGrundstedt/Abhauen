using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        PlayerIsDying,
        GameOver
    }
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
}

