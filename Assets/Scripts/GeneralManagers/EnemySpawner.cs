using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    GameObject selectedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Screen.width);
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy()
    {
        int randomNbrOfEnemies = RNG.Instance.IntRNG(2, 5);
        float spawnOffset = 6.5f;

        //Debug.Log("Nbr of enemies: " + randomNbrOfEnemies);

        for (int i = 0; i < randomNbrOfEnemies; i++)
        {
            Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnOffset;

            Instantiate(EnemySelection(), spawnPos, Quaternion.identity);
        }
    }

    private GameObject EnemySelection()
    {
        
        int nbrOfEnemies = enemyPrefab.Length;

        int x = RNG.Instance.IntRNG(1, nbrOfEnemies + 1);

        switch (x)
        {
            case 1:
                selectedEnemy = enemyPrefab[0];
                break;
            case 2:
                selectedEnemy = enemyPrefab[1];
                break;
            case 3:
                selectedEnemy = enemyPrefab[2];
                break;
            default:
                Debug.Log("Value not legal!");
                break;

        }

        return selectedEnemy;
    }
}
