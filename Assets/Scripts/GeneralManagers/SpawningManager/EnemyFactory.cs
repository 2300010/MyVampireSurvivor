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

    public GameObject CreateWeakEnemy(Vector2 position, Quaternion rotation)
    {
        //Debug.Log("Weak enemy created");
        //GameObject weakEnemy = ObjectPoolingSystem.Instance().GetPoolObject("SkeletonSoldierPrefab");
        GameObject weakEnemy = Instantiate(weakEnemyPrefab, position, rotation);
        weakEnemy.transform.SetPositionAndRotation(position, rotation);
        
        return weakEnemy;
    }

    public GameObject CreateAverageEnemy(Vector2 position, Quaternion rotation)
    {
        //GameObject averageEnemy = ObjectPoolingSystem.Instance().GetPoolObject("OfficerSkeletonPrefab");
        GameObject averageEnemy = Instantiate(averageEnemyPrefab, position, rotation);
        averageEnemy.transform.SetPositionAndRotation(position, rotation);

        return averageEnemy;
    }

    public GameObject CreateStrongEnemy(Vector2 position, Quaternion rotation)
    {
        //GameObject strongEnemy = ObjectPoolingSystem.Instance().GetPoolObject("MistKnightPrefab");
        GameObject strongEnemy = Instantiate(strongEnemyPrefab, position, rotation);
        //Debug.Log("Enemy is : " + strongEnemy);
        strongEnemy.transform.SetPositionAndRotation(position, rotation);

        return strongEnemy;
    }

}
