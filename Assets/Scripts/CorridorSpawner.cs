using System;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject corridorPrefab;

    [SerializeField] private int corridorPartCount;
    [SerializeField] private float corridorPartZLength;

    [SerializeField] private Transform lastPart;

    public static event Action<Vector3> OnCorridorPartSpawned;

    private void Awake()
    {
        CorridorPart.OnPartDestroyed += SpawnNewPart;
    }

    private void SpawnNewPart(Vector3 destroyedPos)
    {
        Vector3 newPos = lastPart.transform.position + new Vector3(0f, 0f, corridorPartZLength);
        lastPart = Instantiate(corridorPrefab, newPos, Quaternion.identity, transform).transform;
        OnCorridorPartSpawned?.Invoke(newPos);
    }

    private void OnDisable()
    {
        CorridorPart.OnPartDestroyed -= SpawnNewPart;
    }
}
