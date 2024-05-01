using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public delegate void EnemyDeath(EnemyManager enemy);
    //public event EnemyDeath OnEnemyDeath;

    HpManager hpManager;

    [SerializeField] private AudioClip clip;

    private void Start()
    {

        hpManager = GetComponent<HpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpManager.IsDead)
        {
            AudioManager.GetInstance().PlaySound(clip);
            gameObject.SetActive(false);
        }
    }

    //public void IsDead()
    //{
    //    OnEnemyDeath?.Invoke(this);
    //}
}
