using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject scythePrefab;
    [SerializeField] float weaponSpawnRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WeaponSpawn(5);
        }
    }

    private void WeaponSpawn(int instancesToSpawn)
    {

        for (int i = 0; i < instancesToSpawn; i++)
        {
            Vector3 randomOffset = Random.insideUnitCircle.normalized * weaponSpawnRadius;
            Vector3 spawnPosition = transform.position + randomOffset;

            Vector3 directionToPlayer = transform.position - spawnPosition;

            GameObject weapon = Instantiate(scythePrefab, spawnPosition, Quaternion.identity);

            WeaponManager weaponManager = weapon.GetComponent<WeaponManager>();
            weaponManager.Direction = directionToPlayer;
        }
    }
}
