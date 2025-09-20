using UnityEngine;

public class Corridor : MonoBehaviour
{
    public static Corridor Instance { get; private set; }

    [field: SerializeField] public float Speed { get; private set; } = 1f;

    private void Awake()
    {
        Instance = this;
    }

}
