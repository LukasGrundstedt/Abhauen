using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int enemyProbability = 25;
    [SerializeField] private GameObject[] enemyPrefabs;

    private GameObject currentEnemy;

    private void Awake()
    {
        CorridorSpawner.OnCorridorPartSpawned += RollEnemySpawn;
    }

    private void RollEnemySpawn(Vector3 position)
    {
        if (currentEnemy != null) return;

        int roll = Random.Range(0, 101);

        if (roll > enemyProbability) return;

        int enemyID = Random.Range(0, enemyPrefabs.Length);
        currentEnemy = Instantiate(enemyPrefabs[enemyID], position, Quaternion.identity);
    }
}