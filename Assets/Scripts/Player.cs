using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float fallSpeed;
    [SerializeField] private int aimScore;
    [SerializeField] private TextMeshProUGUI drugsAmount;
    [SerializeField] private Image drugClosed;
    [SerializeField] private Image drugOpened;

    [SerializeField] private int maxHealth = 10;
    public int DrugsAvailable = 5;

    public int Score { get; set; } = 0;
    public int Health { get; set; }
    public int DrugsUsed { get; private set; } = 0;
    public bool OnDrugs { get; set; } = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        Score = aimScore;
        Health = maxHealth;
        DrugsUsed = 0;
        sprite.transform.rotation = Quaternion.identity;
        SetDrugOpenedActive(true);
        drugsAmount.text = (DrugsAvailable - DrugsUsed).ToString();
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

        if (OnDrugs != false)
        {
            drugClosed.color = Color.red;
            StartCoroutine(HideDragsUseable());
            return;
        }

        OnDrugs = true;
        DrugsUsed++;
        Health++;
        SetDrugOpenedActive(false);
        drugsAmount.text = (DrugsAvailable - DrugsUsed).ToString();
        SpeedController.Instance.SetSlowFactor();
    }

    public IEnumerator HideDragsUseable()
    {
        yield return new WaitForSeconds(0.1f);
        drugClosed.color = Color.white;
    }

    public void SetDrugOpenedActive(bool value)
    {
        drugClosed.gameObject.SetActive(!value);
        drugOpened.gameObject.SetActive(value);
    }

    public void GetHit(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;
        Debug.Log("Player got hit! Current health: " + Health);
        if (Health <= 0)
        {
            Health = 0;
            GameManager.Instance.State = GameState.PlayerIsDying;
        }
    }

    public void Die()
    {
        if (GameManager.Instance.State != GameState.PlayerIsDying)
            return;

        Quaternion target = Quaternion.Euler(90, 0, 0);

        if (Quaternion.Angle(sprite.transform.rotation, target) < 0.1f)
        {
            GameManager.Instance.State = GameState.GameOver;
            return;
        }

        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, target, Time.deltaTime * fallSpeed);
    }
}
