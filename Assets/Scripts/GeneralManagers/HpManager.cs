using UnityEngine;

public class HpManager : MonoBehaviour
{
    public delegate void OnEnemyDeath(Vector2 deathPosition, int expGiven);
    public delegate void OnPlayerDeath();


    public static event OnEnemyDeath EnemyDeath;
    public static event OnPlayerDeath PlayerDeath;

    [SerializeField] int maxHp;
    private int currentHp;

    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }

    public void TakeDamage(int damage)
    {

        CurrentHp -= damage;

        if (CurrentHp <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                EnemyManager enemyManager = gameObject.GetComponent<EnemyManager>();
                IsDead(transform.position, enemyManager.ExpDropped);
            }
        }

        //Debug.Log("Character " + gameObject.name + "'s current hp = " + currentHp);
    }

    private void IsDead(Vector2 deathPosition, int expGiven)
    {
        CurrentHp = 0;
        if (gameObject.CompareTag("Enemy"))
        {
            EnemyDeath?.Invoke(deathPosition, expGiven);
        }
        else
        {
            PlayerDeath?.Invoke();
        }

        gameObject.SetActive(false);
    }


}
