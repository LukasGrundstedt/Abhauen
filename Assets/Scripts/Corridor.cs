using UnityEngine;

public class Corridor : MonoBehaviour
{
    public static Corridor Instance { get; private set; }
    
    public static float Speed { get; private set; }
    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        Instance = this;
        Speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Speed = speed;
    }
}
