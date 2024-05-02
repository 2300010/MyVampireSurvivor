using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNG : MonoBehaviour
{
    private static RNG instance;

    private System.Random random;

    public static RNG Instance => instance;

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

        random = new System.Random();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int IntRNG(int minValue, int maxValue) => random.Next(minValue, maxValue);

    public float FloatRNG(float minValue, float maxValue) => (float)random.NextDouble() * (maxValue - minValue) + minValue;
}
