using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
}

[Serializable]
public class WaveSystem
{
    public List<EnemyType> enemies;
    public float spawnInterval;
    public float timeBetweenWaves;
}

public class EnemySpawner : MonoBehaviour
{
    public List<WaveSystem> waves;
    private int waveIndex = 0;

    const string WEAK_ENEMY = "SkeletonSoldierPrefab";
    const string AVERAGE_ENEMY = "OfficerSkeletonPrefab";
    const string STRONG_ENEMY = "MistKnightPrefab";

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WaveSystem());
        //SpawnEnemy(waves[0].enemies[0].enemyPrefab);
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        float spawnOffset = 10f;
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnOffset;
        if (enemyPrefab != null)
        {
            if(enemyPrefab.name == WEAK_ENEMY)
            {
                enemyPrefab = EnemyFactory.Instance.CreateWeakEnemy(spawnPos, Quaternion.identity);
                enemyPrefab.SetActive(true);
            }
            else if(enemyPrefab.name == AVERAGE_ENEMY)
            {
                enemyPrefab = EnemyFactory.Instance.CreateAverageEnemy(spawnPos, Quaternion.identity);
                enemyPrefab.SetActive(true);
            }
            else if(enemyPrefab.name == STRONG_ENEMY)
            {
                enemyPrefab = EnemyFactory.Instance.CreateStrongEnemy(spawnPos, Quaternion.identity);
                enemyPrefab.SetActive(true);
            }
        }
    }

    IEnumerator WaveSystem()
    {
        while (waveIndex < waves.Count)
        {
            WaveSystem wave = waves[waveIndex];
            foreach (EnemyType enemyType in wave.enemies)
            {
                //Debug.Log(enemyType.enemyPrefab.name);
                SpawnEnemy(enemyType.enemyPrefab);
                yield return new WaitForSeconds(wave.spawnInterval);
            }
            waveIndex++;
            
            if(waveIndex >= waves.Count)
            {
                yield return new WaitForSeconds(wave.timeBetweenWaves);
            }
        }
    }
}
