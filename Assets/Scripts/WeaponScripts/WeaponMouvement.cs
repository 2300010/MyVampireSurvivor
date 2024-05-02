using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMouvement : MonoBehaviour
{

    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.position * speed * Time.deltaTime;
    }
}
