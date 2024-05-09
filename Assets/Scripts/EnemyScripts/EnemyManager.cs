using UnityEngine;

public class EnemyManager : MonoBehaviour, Ipoolable
{

    [SerializeField] HpManager hpManager;

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
        HpManager.EnemyDeath += OnDeath;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        //Debug.Log("Collision with :" + collider.gameObject.name);

        if (collider.CompareTag("Player"))
        {
            DealDamage();
        }
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
        //Debug.Log("Damage dealt = " + damage);
        //Debug.Log("Player Hp = " + PlayerManager.Instance().GetComponent<HpManager>().CurrentHp);
    }

}
