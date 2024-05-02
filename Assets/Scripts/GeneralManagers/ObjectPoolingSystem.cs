using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ipoolable
{
    void Reset();
}

public class ObjectPoolingSystem : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolSize;
    int poolIndex;
    
    List<GameObject> poolOfObjects = new();

    public static ObjectPoolingSystem instance;

    public static ObjectPoolingSystem GetInstance() => instance;    

    public GameObject ObjectToPool { set => objectToPool = value; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolSize; i++) 
        {
            GameObject go = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, transform);
            go.SetActive(false);
            poolOfObjects.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPoolObject()
    {
        poolIndex %= poolSize;
        return poolOfObjects[poolIndex++];
    }
}
