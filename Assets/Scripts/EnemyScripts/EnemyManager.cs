using UnityEngine;

public class EnemyManager : MonoBehaviour, Ipoolable
{

    HpManager hpManager;
    EnemyMouvement enemyMouvement;

    [SerializeField] AudioClip clip;
    [SerializeField] int expDropped;
    [SerializeField] int damage;

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
        HpManager.EnemyDeath += OnDeath;
    }


    public void OnDeath(Vector2 pos, int expDropped)
    {
        AudioManager.GetInstance().PlaySound(clip);
        HpManager.EnemyDeath -= OnDeath;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        //Debug.Log("Collision with :" + collider.gameObject.name);

        if (collider.CompareTag("Player"))
        {
            DealDamage(PlayerManager.Instance.gameObject, damage);
            Debug.Log("Damage dealt = " + damage);
            Debug.Log("Player Hp = " + PlayerManager.Instance.GetComponent<HpManager>().CurrentHp);
        }
    }

    private void DealDamage(GameObject opponent, int damage)
    {
        HpManager opponentHpManager = opponent.GetComponent<HpManager>();
        opponentHpManager.TakeDamage(damage);
    }
}
