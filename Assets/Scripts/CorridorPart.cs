using System;
using UnityEngine;

public class CorridorPart : MonoBehaviour
{
    public float Speed { get; set; } = 1f;

    [SerializeField] private Rigidbody rb;

    public static event Action<Vector3> OnPartDestroyed;

    private void Update()
    {
        rb.MovePosition(transform.position + new Vector3(0f, 0f, -(Corridor.Speed * Time.deltaTime)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "KillPlane")
        {
            OnPartDestroyed?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }
}
