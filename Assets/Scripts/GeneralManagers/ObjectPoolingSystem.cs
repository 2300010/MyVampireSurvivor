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
    public List<GameObject> poolOfObjects = new();
}

public class ObjectPoolingSystem : MonoBehaviour
{
    [SerializeField] List<PoolInfo> objectPools;
    int poolIndex;

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

        foreach (var pool in objectPools)
        {
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject go = Instantiate(pool.objectToPool, Vector3.zero, Quaternion.identity, transform);
                go.SetActive(false);
                pool.poolOfObjects.Add(go);
            }
        }
    }

    public GameObject GetPoolObject(string objectName)
    {
        foreach (var pool in objectPools)
        {
            if (objectName == pool.objectToPool.name)
            {

                poolIndex %= pool.poolSize;
                return pool.poolOfObjects[poolIndex++];
            }
        }
        return null;
    }

    public void ReturnPoolObject(GameObject objectToReturn)
    {
        foreach (PoolInfo pool in objectPools)
        {
            if (objectToReturn.name == pool.objectToPool.name)
            {
                pool.poolOfObjects.Add(objectToReturn);
            }
        }
    }
}
