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
        GameObject weakEnemy = ObjectPoolingSystem.Instance().GetPoolObject("Skeleton Soldier");
        weakEnemy.transform.position = position;
        weakEnemy.transform.rotation = rotation;
        
        return weakEnemy;
    }
    public GameObject CreateAverageEnemy(Vector2 position, Quaternion rotation)
    {
        GameObject averageEnemy = ObjectPoolingSystem.Instance().GetPoolObject("Officer Skeleton");
        averageEnemy.transform.position = position;
        averageEnemy.transform.rotation = rotation;

        return averageEnemy;
    }
    public GameObject CreateStrongEnemy(Vector2 position, Quaternion rotation)
    {
        GameObject strongEnemy = ObjectPoolingSystem.Instance().GetPoolObject("Mist Knight");
        strongEnemy.transform.position = position;
        strongEnemy.transform.rotation = rotation;

        return strongEnemy;
    }

}
