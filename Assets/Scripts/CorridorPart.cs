using System;
using UnityEngine;

public class CorridorPart : MonoBehaviour
{
    public static event Action<Vector3> OnPartDestroyed;

    private void OnDestroy()
    {
        OnPartDestroyed?.Invoke(transform.position);
    }
}
