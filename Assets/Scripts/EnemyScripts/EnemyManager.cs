using UnityEngine;

public class EnemyManager : MonoBehaviour, Ipoolable
{

    HpManager hpManager;
    EnemyMouvement enemyMouvement;

    [SerializeField] AudioClip clip;
    [SerializeField] int expDropped;

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
    }

    
}
