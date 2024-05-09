using System.Collections;
using UnityEngine;

public class WeaponSpawning : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] float attackSpeed;
    [SerializeField] float spawnRate;
    [SerializeField] int nbrOfInstances;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    private IEnumerator SpawnWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);

            Vector3 position = transform.position;

            for (int i = 0; i < nbrOfInstances; i++)
            {
                //float direction = PlayerMouvement.Instance().transform.localScale.x > 0 ? 1 : -1;

                //Debug.Log("Direction = " + direction);

                //Vector3 spawnPosition = position + Vector3.right * direction;

                Instantiate(weapon, position, Quaternion.identity);

                yield return new WaitForSeconds(spawnRate);
            }
        }
        //weapon.SetActive(false);
    }
}
