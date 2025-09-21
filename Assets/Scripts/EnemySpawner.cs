using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int enemyProbability = 25;
    [SerializeField] private GameObject[] enemyPrefabs;
    private int currentIndex = 0;

    private Enemy currentEnemy;

    private void Awake()
    {
        CorridorSpawner.OnCorridorPartSpawned += RollEnemySpawn;
        currentIndex = 0;
    }

    private void RollEnemySpawn(Vector3 position)
    {
        if (currentEnemy != null) return;
        if (currentIndex == enemyPrefabs.Length) return;

        int roll = Random.Range(0, 101);

        if (roll > enemyProbability) return;

        currentEnemy = Instantiate(enemyPrefabs[currentIndex], position, Quaternion.identity).GetComponent<Enemy>();
        currentIndex++;

        // Offset enemy to either left or right side of the corridor
        int xOffset = Random.Range(0, 2);
        if (xOffset == 1)
        {

        }
        else // 0
        {
            xOffset = -1;
        }
        Vector3 newPos = currentEnemy.SpriteRenderer.transform.position;
        newPos.x = xOffset;
        currentEnemy.SpriteRenderer.transform.position = newPos;
    }
}