using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    private RNG localRNG;

    private void Awake()
    {
        enemyPrefab = new GameObject[enemyPrefab.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Instantiate(EnemySelection(), new Vector2(3f, 3f), Quaternion.identity); 
    }

    private GameObject EnemySelection()
    {
        GameObject selectedEnemy;

        switch (RNG.Instance.IntRNG(1, enemyPrefab.Length))
        {

        }

        return selectedEnemy;
    }
}
