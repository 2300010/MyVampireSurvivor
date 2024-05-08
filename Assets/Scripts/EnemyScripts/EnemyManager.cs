using System.Collections;
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
    [SerializeField] int attackCooldownTime;

    public int ExpDropped { get => expDropped; set => expDropped = value; }
    private void OnEnable()
    {
    }

    private void Start()
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
        enemyAISensor.InRangeToAttackAction += StartAttacking;
    }


    public void OnDeath(Vector2 pos, int expDropped)
    {
        AudioManager.GetInstance().PlaySound(clip);
        gameObject.SetActive(false);
    }

    public void DealDamage()
    {
        GameObject opponent = PlayerManager.Instance().gameObject;
        HpManager opponentHpManager = opponent.GetComponent<HpManager>();
        opponentHpManager.TakeDamage(damage);
        Debug.Log("Damage dealt = " + damage);
        Debug.Log("Player Hp = " + PlayerManager.Instance().GetComponent<HpManager>().CurrentHp);
    }

    private void StartAttacking()
    {
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        enemyMouvement.Speed = 0;

        while (true)
        {
            if (enemyMouvement.Speed <= 0)
            {
                Attack();
            }
            else
            {
                StopCoroutine(AttackCooldown());
                break;
            }
            yield return new WaitForSeconds(attackCooldownTime);
        }

    }

    public void Attack()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
