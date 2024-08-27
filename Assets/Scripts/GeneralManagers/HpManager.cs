using UnityEngine;

public class HpManager : MonoBehaviour
{
    public delegate void OnPlayerDeath();

    public static event OnPlayerDeath PlayerDeath;

    [SerializeField] int maxHp;
    private int currentHp;
    EnemyManager enemyManager;

    #region Unity Methods

    private void OnEnable()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            enemyManager = gameObject.GetComponent<EnemyManager>();
        }
    }

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
                    enemyManager.OnDeath();
                }
                else if (gameObject.CompareTag("Player"))
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
