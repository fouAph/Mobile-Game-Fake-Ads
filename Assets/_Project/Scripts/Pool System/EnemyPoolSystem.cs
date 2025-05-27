using UnityEngine;

public class EnemyPoolSystem : GenericPoolManager<EnemyController>
{
    public static EnemyPoolSystem Instance;
    protected override void Awake()
    {
        Instance = this;
        base.Awake();
    }

    public void SpawnEnemyAtPosition(string enemyName, Vector3 spawnPosition)
    {
        var e = SpawnFromPool(enemyName);
        e.OnEnemySpawn();
        e.transform.position = spawnPosition;
    }
}

