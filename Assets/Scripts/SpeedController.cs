using System.Collections;
using TMPro;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance { get; private set; }

    [SerializeField] private float maxMultiplier = 10f;
    [SerializeField] private int maxCountToMaxSpeed = 21; // 75% of 28
    [SerializeField] private int countToNormal = 3;
    [SerializeField] private float drugSlowFactor = 0.5f;
    [SerializeField] private float textSlowFactor = 0.5f;
    public float LevelSpeed { get; private set; }
    public float TextObjectSpeed { get; private set; }
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
        float soundSpeed = Mathf.Lerp(0f, 0.9f, (float)Count / (float)maxCountToMaxSpeed);
        AudioManager.Instance.SetIntensity(soundSpeed);
        LevelSpeed = Corridor.Instance.Speed * currentMultiplier;
        TextObjectSpeed = LevelSpeed * CurrentFactor * textSlowFactor;

        Debug.Log("Speed: " + LevelSpeed + " | Count: " + Count + " | Multiplier: " + currentMultiplier + " | CurrentFactor: " + CurrentFactor);
    }

    public void UpdateCount()
    {
        CountDrugs();
        if (Count < maxCountToMaxSpeed)
        {
            Count++;
        }
    }

    public void SetSlowFactor()
    {
        CurrentFactor = drugSlowFactor;
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
            Player.Instance.SetDrugOpenedActive(true);
            return;
        }
        CountWhileDrugs++;
        float ratio = (float)CountWhileDrugs / (float)countToNormal;
        CurrentFactor =  ratio * (1-drugSlowFactor) + drugSlowFactor;
    }
}

