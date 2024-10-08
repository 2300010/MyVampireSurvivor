using UnityEngine;

public class HpManager : MonoBehaviour
{
    public delegate void OnPlayerDeath();

    public static event OnPlayerDeath PlayerDeath;

    public delegate void OnPlayerDamaged();

    public static event OnPlayerDamaged PlayerIsTakingDamage;

    [SerializeField] private int currentHp;
    private EnemyManager enemyManager;
    private ObjectTag currentObjectTag;

    #region Unity Methods

    private void OnEnable()
    {
        if (gameObject.CompareTag(ObjectTag.Enemy.ToString()))
        {
            currentObjectTag = ObjectTag.Enemy;
            enemyManager = gameObject.GetComponent<EnemyManager>();
        }
        else
        {
            currentObjectTag = ObjectTag.Player;
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

                if (currentObjectTag == ObjectTag.Enemy)
                {
                    enemyManager.OnDeath();
                }
                else if (currentObjectTag == ObjectTag.Player)
                {
                    PlayerDeath?.Invoke();
                }
            }
            currentHp = value;
        }
    }
    #endregion

    #region Custom Methods
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if(currentObjectTag == ObjectTag.Enemy)
        {
            enemyManager.OnDamageTaken();
        }
        else if (currentObjectTag == ObjectTag.Player)
        {
            PlayerIsTakingDamage?.Invoke();
        }
        Debug.Log("Character " + gameObject.name + "'s current hp = " + currentHp);
    }
    #endregion
}
