using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] int maxHp;
    private int currentHp;
    private bool isDead;
    //EnemyManager thisEnemyManager;

    public bool IsDead { get => isDead; set => isDead = value; }

    private void OnEnable()
    {
        currentHp = maxHp;
        IsDead = false;
        //thisEnemyManager = GetComponent<EnemyManager>();
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

        //Debug.Log("Character " + gameObject.name + "'s current hp = " + currentHp);
    }

    
}
