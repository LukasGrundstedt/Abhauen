using System.Collections;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance { get; private set; }

    [SerializeField] private float maxMultiplier = 10f;
    [SerializeField] private int maxCountToMaxSpeed = 21; // 75% of 28
    [SerializeField] private int countToNormal = 3;
    [SerializeField] private float slowDownFactor = 0.5f;
    public float Speed { get; private set; }
    public int Count { get; set; } = 0;
    public int CountWhileDrugs { get; set; } = 0;
    public float CurrentFactor { get; set; } = 1f;
   

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    private void Update()
    {
        float currentMultiplier = Mathf.Lerp(1f, maxMultiplier, (float)Count / (float)maxCountToMaxSpeed);
        Speed = Corridor.Instance.Speed * currentMultiplier * CurrentFactor * Time.deltaTime;

        Debug.Log("Speed: " + Speed + " | Count: " + Count + " | Multiplier: " + currentMultiplier + " | CurrentFactor: " + CurrentFactor);
    }

    public void UpdateCount()
    {
        CountDrugs();
        if (Count < maxCountToMaxSpeed)
            Count++;
    }

    public void SetSlowFactor()
    {
        CurrentFactor = slowDownFactor;
    }

    public void CountDrugs()
    {
        if (!Player.Instance.OnDrugs)
            return;

        if (CountWhileDrugs >= countToNormal)
        {
            CountWhileDrugs = 0;
            CurrentFactor = 1f;
            Player.Instance.OnDrugs = false;
            return;
        }
        CountWhileDrugs++;
        float ratio = (float)CountWhileDrugs / (float)countToNormal;
        CurrentFactor = (1f - ratio) * slowDownFactor;
    }
}

