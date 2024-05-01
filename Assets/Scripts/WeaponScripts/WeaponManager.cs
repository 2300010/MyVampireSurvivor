using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] int weaponDamage;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision with " + collision.gameObject.name);

        Collider2D collider = collision.collider;

        if (!collider.CompareTag("Player"))
        {
            DealDamage(collider.GameObject(), weaponDamage);
            Destroy(gameObject);
        }
    }

    private void DealDamage(GameObject opponent, int damage)
    {
        HpManager opponentHpManager = opponent.GetComponent<HpManager>();
        opponentHpManager.TakeDamage(damage);
    }
}
