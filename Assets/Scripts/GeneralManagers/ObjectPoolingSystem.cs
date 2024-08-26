using System;
using System.Collections.Generic;
using UnityEngine;

public interface Ipoolable
{
    void Reset();
}

[Serializable]
public class PoolInfo
{
    public GameObject objectToPool;
    public int poolSize;
    public List<GameObject> poolOfObjects;
}

public class ObjectPoolingSystem : MonoBehaviour
{
    [SerializeField] List<PoolInfo> objectPools;
    readonly Dictionary<string, PoolInfo> poolDictionary = new Dictionary<string, PoolInfo>();
    int poolIndex = 0;

    public static ObjectPoolingSystem instance;

    public static ObjectPoolingSystem Instance() => instance;

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

    // Start is called before the first frame update
    void Start()
    {

        foreach (PoolInfo pool in objectPools)
        {
            poolDictionary[pool.objectToPool.name] = pool;
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject go = Instantiate(pool.objectToPool, Vector3.zero, Quaternion.identity, transform);
                go.SetActive(false);
                pool.poolOfObjects.Add(go);
                //Debug.Log("Game object " + go.name + " added to pool");
            }
        }
    }

    public GameObject GetPoolObject(string objectName)
    {
        GameObject objectToReturn = null;
        bool inactiveObjectFound = false;
        //if (poolDictionary.ContainsKey(objectName))
        //{
        //    poolIndex = 0;

        //    while (inactiveObjectFound)
        //    {
        //        if (!poolDictionary[objectName].poolOfObjects[poolIndex].SetActiveRecursively())
        //        {
        //            objectToReturn = poolDictionary[objectName].poolOfObjects[poolIndex];
        //        }
        //        else
        //        {
        //            poolIndex++;
        //        }
        //    }

        //}
        return objectToReturn;
    }

    public void ReturnPoolObject(GameObject objectToReturn)
    {
        if (poolDictionary.TryGetValue(objectToReturn.name, out PoolInfo pool))
        {
            pool.poolOfObjects.Add(objectToReturn);
        }
        else
        {
            Debug.LogWarning("Pool for object " + objectToReturn.name + " not found.");
        }
    }
}
