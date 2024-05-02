using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject scythePrefab;
    [SerializeField] float weaponSpawnRadius;

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
            Quaternion rotation = Quaternion.Euler(0f, 0f, RNG.Instance.FloatRNG(0, 360));

            GameObject weaponToSpawn = ObjectPoolingSystem.GetInstance().GetPoolObject();
            weaponToSpawn.transform.SetPositionAndRotation(transform.position, rotation);

            weaponToSpawn.SetActive(true);  
        }
    }
}
