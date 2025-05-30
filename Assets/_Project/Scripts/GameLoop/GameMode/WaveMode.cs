using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;
public class WaveMode : GameModeBase
{
    [SerializeField] WaveSetting[] waveSettings;
    [SerializeField] float spawnRate = 2f;

    [ReadOnly] int currentWaveCount;
    [ReadOnly] int currentEnemyLeft;
    [ReadOnly] int aliveEnemyCount;
    private float lastSpawn;
    public override void StartGame()
    {
        Debug.Log("Wave Mode Started");
        currentEnemyLeft = waveSettings[currentWaveCount].maxEnemy;
        isGameStarted = true;
    }

    public override void GameUpdate()
    {
        if (isGameStarted == false) return;
        if (currentEnemyLeft == 0) return;
        if (Time.time - lastSpawn / 1 > spawnRate)
        {
            // Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], GetRandomPositionFromBoxCollider(), Quaternion.identity);

            GameManager.Instance.SpawnEnemy("Zombie");

            lastSpawn = Time.time;
            currentEnemyLeft--;

        }
    }

    public override void EndGame()
    {
        Debug.Log("Wave Mode Ended");
    }

    public override void OnEnemyDie()
    {
        GameManager.Instance.AddCredit(waveSettings[currentWaveCount].creditWhenKillingEnemy);
        aliveEnemyCount--;

        if (aliveEnemyCount == 0)
        {
            StartCoroutine(NextWave());
        }
    }

    public IEnumerator NextWave()
    {
        if (currentWaveCount == waveSettings.Length)
        {
            yield break;
        }
        currentWaveCount++;

        currentEnemyLeft = waveSettings[currentWaveCount].maxEnemy;
        isGameStarted = true;
    }

    [Serializable]
    public struct WaveSetting
    {
        public int maxEnemy;
        public int creditWhenKillingEnemy;
        public string enemyName;
    }
}
