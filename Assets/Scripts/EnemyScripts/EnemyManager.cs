using System.Threading;
using UnityEngine;

public class EnemyManager : MonoBehaviour, Ipoolable
{

    HpManager hpManager;
    EnemyMouvement enemyMouvement;
    EnemyAISensor enemyAISensor;

    [SerializeField] GameObject projectile;
    [SerializeField] AudioClip clip;
    [SerializeField] int expDropped;
    [SerializeField] int damage;
    [SerializeField] int attackCooldown;

    public int ExpDropped { get => expDropped; set => expDropped = value; }
    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        hpManager = GetComponent<HpManager>();
        hpManager.CurrentHp = hpManager.MaxHp;
        enemyMouvement = GetComponent<EnemyMouvement>();
        enemyMouvement.Speed = enemyMouvement.BaseSpeed;
        enemyAISensor = GetComponent<EnemyAISensor>();
        HpManager.EnemyDeath += OnDeath;
        enemyAISensor.InRangeToAttackAction += AttackCooldown;
    }


    public void OnDeath(Vector2 pos, int expDropped)
    {
        AudioManager.GetInstance().PlaySound(clip);
        gameObject.SetActive(false);
    }

    public void DealDamage()
    {
        GameObject opponent = PlayerManager.Instance.gameObject;
        HpManager opponentHpManager = opponent.GetComponent<HpManager>();
        opponentHpManager.TakeDamage(damage);
        Debug.Log("Damage dealt = " + damage);
        Debug.Log("Player Hp = " + PlayerManager.Instance.GetComponent<HpManager>().CurrentHp);
    }

    private void AttackCooldown()
    {
        float timer = attackCooldown;
        while (true)
        {
            if (timer < attackCooldown)
            {
                timer++;
            }
            else
            {
                Attack();
                timer = 0;
            }
        }
    }

    public void Attack()
    {
        enemyMouvement.Speed = 0;
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
