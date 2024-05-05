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
}

public class ObjectPoolingSystem : MonoBehaviour
{
    //[SerializeField] List<PoolInfo> objectPools;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolSize;
    int poolIndex;

    List<GameObject> poolOfObjects = new();

    public static ObjectPoolingSystem instance;

    public static ObjectPoolingSystem Instance() => instance;

    public GameObject ObjectToPool { set => objectToPool = value; }

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
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, transform);
            go.SetActive(false);
            poolOfObjects.Add(go);
        }
    }

    public GameObject GetPoolObject()
    {
        poolIndex %= poolSize;
        return poolOfObjects[poolIndex++];
    }

    public void ReturnPoolObject(GameObject objectToReturn)
    {
        poolOfObjects.Add(objectToReturn);
    }
}
