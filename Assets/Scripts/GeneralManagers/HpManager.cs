using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] int maxHp;
    private int currentHp;
    private bool isDead;
    //EnemyManager thisEnemyManager;

    public bool IsDead { get => isDead; set => isDead = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        currentHp = maxHp;
        IsDead = false;
        //thisEnemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {

        currentHp -= damage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            IsDead = true;
            //thisEnemyManager.IsDead();
        }

        //Debug.Log(gameObject.name + " CurrentHp = " + currentHp);
    }

    
}
