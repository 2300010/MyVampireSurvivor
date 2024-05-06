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


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveSystem());
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        float spawnOffset = 10f;
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnOffset;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    IEnumerator WaveSystem()
    {
        while (waveIndex < waves.Count)
        {
            WaveSystem wave = waves[waveIndex];
            foreach (EnemyType enemyType in wave.enemies)
            {
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
