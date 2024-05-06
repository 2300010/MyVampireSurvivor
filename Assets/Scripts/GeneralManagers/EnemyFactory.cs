using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory instance;
    public static EnemyFactory Instance => instance;

    [SerializeField] GameObject weakEnemyPrefab;
    [SerializeField] GameObject averageEnemyPrefab;
    [SerializeField] GameObject strongEnemyPrefab;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject CreateWeakEnemy(Vector2 position, Quaternion rotation)
    {
        
        return Instantiate(weakEnemyPrefab, position, rotation);
    }
    public GameObject CreateAverageEnemy(Vector2 position, Quaternion rotation)
    {
        return Instantiate(weakEnemyPrefab, position, rotation);
    }
    public GameObject CreateStrongEnemy(Vector2 position, Quaternion rotation)
    {
        return Instantiate(weakEnemyPrefab, position, rotation);
    }

}
