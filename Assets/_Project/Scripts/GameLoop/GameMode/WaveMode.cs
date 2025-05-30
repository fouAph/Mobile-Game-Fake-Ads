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
    [ReadOnly] int enemyAliveCount;
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
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (Time.time - lastSpawn / 1 > spawnRate)
        {
            // Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], GetRandomPositionFromBoxCollider(), Quaternion.identity);
            enemyAliveCount++;

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
        enemyAliveCount--;

        if (enemyAliveCount == 0)
        {
            StartCoroutine(NextWave());
        }
    }

    public IEnumerator NextWave()
    {
        if (currentWaveCount < waveSettings.Length - 1)
        {

            print("New Wave is Coming");
            yield return new WaitForSeconds(1f);
            currentWaveCount++;

            currentEnemyLeft = waveSettings[currentWaveCount].maxEnemy;
            isGameStarted = true;
        }
        else
        {
            EndGame();
        }
    }

    [Serializable]
    public struct WaveSetting
    {
        public int maxEnemy;
        public int creditWhenKillingEnemy;
        public string enemyName;
    }
}
