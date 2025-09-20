using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float fallSpeed;
    [SerializeField] private int aimScore;
    public int MaxHealth = 10;
    public int DrugsAvailable = 5;

    public int Score { get; set; } = 0;
    public int Health { get; set; }
    public int DrugsUsed { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        Score = aimScore;
        Health = MaxHealth;
        DrugsUsed = 0;
        sprite.transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        Die();
    }

    public void MakeScore()
    {
        if (Score >= aimScore)
            return;

        Score++;
    }

    public void TakeDrugs()
    {
        if (DrugsUsed > 5)
            return;

        DrugsUsed++;
        Health++;
    }

    public void GetHit(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;
    }

    public void Die()
    {
        if (GameManager.Instance.State != GameState.PlayerIsDying)
            return;

        Quaternion target = Quaternion.Euler(90, 0, 0);

        if (Quaternion.Angle(transform.rotation, target) < 0.1f)
        {
            GameManager.Instance.State = GameState.GameOver;
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * fallSpeed);
    }
}
