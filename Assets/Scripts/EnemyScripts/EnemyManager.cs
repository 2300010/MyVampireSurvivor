using UnityEngine;

public class EnemyManager : MonoBehaviour//, Ipoolable
{

    [SerializeField] HpManager hpManager;

    [SerializeField] AudioClip clip;
    [SerializeField] int expDropped;
    [SerializeField] int damage;

    public int ExpDropped { get => expDropped; set => expDropped = value; }

    //private void Start()
    //{

    //}

    //public void Reset()
    //{

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;

        //Debug.Log("Collision with :" + collider.gameObject.name);

        if (collider.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    public void OnDeath()
    {
        AudioManager.GetInstance().PlaySound(clip);
        if (RNG.Instance.FloatRNG(0, 1) < GameManager.Instance.DropRate)
        {
            ExpFlameDrop();
        }
        gameObject.SetActive(false);
    }

    private void ExpFlameDrop()
    {
        if (GameManager.Instance.ExpPrefab != null)
        {
            GameObject expPrefab = GameManager.Instance.ExpPrefab;

            GameObject expFlame = Instantiate(expPrefab, transform.position, Quaternion.identity);

            ExpFlameManager flameManager = expFlame.GetComponent<ExpFlameManager>();
            if (flameManager != null)
            {
                flameManager.ExpGiven = expDropped;
                //Debug.Log("Exp given = " + expGiven);
            }
        }
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
