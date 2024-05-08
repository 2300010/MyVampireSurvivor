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
        
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            EnemyAISensor tempSensor = child.GetComponent<EnemyAISensor>();
            if (!tempSensor.RangeCollider)
            {
                tempSensor.OnTriggerEnterAction += DealDamage;
            }
        }
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
}
