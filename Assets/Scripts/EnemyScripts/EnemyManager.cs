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
    [SerializeField] float attackCooldownTime;
    float timer = 0;

    public int ExpDropped { get => expDropped; set => expDropped = value; }

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
        enemyAISensor.InRangeToAttackAction += Attack;
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

    public void Attack()
    {
        enemyMouvement.Speed = 0;
        //Debug.Log("Timer = " + timer);

        if (timer >= attackCooldownTime)
        {
            //Debug.Log("Enemy spawning");
            SpawnProjectile();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    private void SpawnProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
