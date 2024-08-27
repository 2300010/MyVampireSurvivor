using System.Collections;
using UnityEngine;

public class WeaponSpawning : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] int numberOfInstances;
    [SerializeField] float spawnTime;
    [SerializeField] float attackSpeed;
    Vector3 direction;

    WeaponManager weaponManager;

    public Vector3 Direction { get => direction; set => direction = value; }

    private void Start()
    {
        direction = spawnPoint.transform.position - transform.position;
        StartCoroutine(SpawnWeapons());
    }

    private IEnumerator SpawnWeapons()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);

            for (int i = 0; i < numberOfInstances; i++)
            {
                SpawnWeapon();
                yield return new WaitForSeconds(spawnTime);
            }
        }
    }

    private void SpawnWeapon()
    {
        Vector3 spawnPosition = transform.position + direction.normalized;

        // Instantiate the weapon prefab
        /*GameObject weapon = */Instantiate(weaponPrefab, spawnPosition, Quaternion.identity);
    }
}
