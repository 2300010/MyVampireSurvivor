using UnityEngine;

public class HpManager : MonoBehaviour
{
    public delegate void OnEnemyDeath(Vector2 deathPosition, int expGiven);
    public delegate void OnPlayerDeath();


    public static event OnEnemyDeath EnemyDeath;
    public static event OnPlayerDeath PlayerDeath;

    [SerializeField] int maxHp;
    private int currentHp;

    #region Unity Methods
    public int CurrentHp
    {
        get => currentHp;
        set
        {
            if (value <= 0)
            {
                value = 0;

                if (gameObject.CompareTag("Enemy"))
                {
                    EnemyManager enemyManager = gameObject.GetComponent<EnemyManager>();
                    EnemyDeath?.Invoke(transform.position, enemyManager.ExpDropped);
                }
                else if(gameObject.CompareTag("Player"))
                {
                    PlayerDeath?.Invoke();
                }
            }
            currentHp = value;
        }
    }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    #endregion

    #region Custom Methods
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;

        Debug.Log("Character " + gameObject.name + "'s current hp = " + currentHp);
    }
    #endregion
}
