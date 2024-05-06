using System;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
