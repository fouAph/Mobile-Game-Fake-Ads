using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] BoxCollider spawnCollider;
    [SerializeField] float spawnRate = 2f;

    private float lastSpawn;
    private void Update()
    {
        if (Time.time - lastSpawn / 1 > spawnRate)
        {
            // Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], GetRandomPositionFromBoxCollider(), Quaternion.identity);

            EnemyPoolSystem.Instance.SpawnEnemyAtPosition("Zombie", GetRandomPositionFromBoxCollider());

            lastSpawn = Time.time;

        }
    }

    public Vector3 GetRandomPositionFromBoxCollider()
    {
        Vector3 pos = new(Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x), spawnCollider.bounds.min.y, Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z));

        return pos;
    }
}