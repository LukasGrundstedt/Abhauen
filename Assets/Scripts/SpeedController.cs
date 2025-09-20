using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance { get; private set; }

    [SerializeField] private float multiplier = 100f;
    public float Speed { get; private set; }
    private float count = 0f;

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
        count += Time.deltaTime;
        Speed = Corridor.Instance.Speed + count * multiplier;
    }

    public void SlowDown()
    {
        count = 0f;
        Speed = Corridor.Instance.Speed;
    }
}

