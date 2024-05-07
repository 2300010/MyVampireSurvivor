using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

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
            GameObject weaponToSpawn = ObjectPoolingSystem.Instance().GetPoolObject("ScythePrefab");

            if (weaponToSpawn != null)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, RNG.Instance.FloatRNG(0, 360));
                
                weaponToSpawn.transform.SetPositionAndRotation(transform.position, rotation);
                
                weaponToSpawn.SetActive(true);
            }
            else
            {
                break;
            }
        }
    }
}
