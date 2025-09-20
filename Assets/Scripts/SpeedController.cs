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
    public bool OnDrugs { get; private set; } = false;

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
        float currentMultiplier = Mathf.Clamp(Count / maxCountToMaxSpeed, 1f, maxMultiplier);
        Speed = Corridor.Instance.Speed * currentMultiplier * CurrentFactor;
    }

    public void CountCount()
    {
        if (Count < maxCountToMaxSpeed)
            Count++;
    }

    public void SetCountDrugs()
    {
        if (CountWhileDrugs != 0)
        {
            // ... Tell player, can't take drugs yet
            return;
        }

        CurrentFactor = slowDownFactor;
        OnDrugs = true;
    }

    public void CheckCountDrugs()
    {
        if (!OnDrugs)
            return;

        if (CountWhileDrugs >= countToNormal)
        {
            CountWhileDrugs = 0;
            CurrentFactor = 1f;
            OnDrugs = false;
            return;
        }
        CountWhileDrugs++;
        CurrentFactor = 1f - (CountWhileDrugs / (float)countToNormal) * (1f - slowDownFactor);
    }
}

