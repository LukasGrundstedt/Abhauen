using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float fallSpeed;
    public int MaxHealth = 10;
    public int DrugsAvailable = 5;


    public int Health { get; set; }
    public int DrugsUsed { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        
        Health = MaxHealth;
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
    }

    public Key GetInput()
    {
        return Key.None;
    }

    public void Die()
    {
        if (GameManager.Instance.State != GameManager.GameState.PlayerIsDying)
            return;

        Quaternion target = Quaternion.Euler(0, 0, 90);

        if (Quaternion.Angle(transform.rotation, target) < 0.1f)
        {
            GameManager.Instance.State = GameManager.GameState.GameOver;
            return;
        }

        sprite.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * fallSpeed);
    }
}
