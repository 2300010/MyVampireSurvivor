using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory instance;
    public static EnemyFactory Instance() => instance;

    [SerializeField] GameObject weakEnemyPrefab;
    [SerializeField] GameObject averageEnemyPrefab;
    [SerializeField] GameObject strongEnemyPrefab;

    private void Awake()
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

    [System.Obsolete]
    public GameObject CreateWeakEnemy(Vector2 position, Quaternion rotation)
    {
        //Debug.Log("Weak enemy created");
        GameObject weakEnemy = ObjectPoolingSystem.Instance().GetPoolObject("SkeletonSoldierPrefab");
        weakEnemy.transform.SetPositionAndRotation(position, rotation);
        
        return weakEnemy;
    }

    [System.Obsolete]
    public GameObject CreateAverageEnemy(Vector2 position, Quaternion rotation)
    {
        GameObject averageEnemy = ObjectPoolingSystem.Instance().GetPoolObject("OfficerSkeletonPrefab");
        averageEnemy.transform.SetPositionAndRotation(position, rotation);

        return averageEnemy;
    }

    [System.Obsolete]
    public GameObject CreateStrongEnemy(Vector2 position, Quaternion rotation)
    {
        GameObject strongEnemy = ObjectPoolingSystem.Instance().GetPoolObject("MistKnightPrefab");
        Debug.Log("Enemy is : " + strongEnemy);
        strongEnemy.transform.SetPositionAndRotation(position, rotation);

        return strongEnemy;
    }

}
