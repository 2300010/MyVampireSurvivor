using UnityEngine;
public enum EnemyName
{
    SkeletonSoldier,
    OfficerSkeleton,
    JrBalrog,
    MistKnight,
    DarkCornian
}

public class EnemyManager : MonoBehaviour//, Ipoolable
{

    [SerializeField] HpManager hpManager;
    [SerializeField] EnemyData enemyData;
    [SerializeField] AudioClip clip;

    [SerializeField] int expDropped;
    [SerializeField] float expDropRate;
    [SerializeField] int damage;

    EnemyMouvement enemyMouvement;
    AnimationManager animationManager;
    EnemyAISensor enemyAISensor;

    public int ExpDropped { get => expDropped; set => expDropped = value; }

    #region Unity Methods
    private void OnEnable()
    {
       
        if (gameObject.name == "MistKnightPrefab")
        {
            enemyAISensor = GetComponent<EnemyAISensor>();
            enemyAISensor.OutOfRangeToAttackAction += enemyMouvement.ChasePlayer;
            enemyAISensor.InRangeToAttackAction += enemyMouvement.StopMoving;
        }
    }

    private void FixedUpdate()
    {
        ManageMouvement();
    }

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
    #endregion

    #region Custom Method

    public void OnDeath()
    {
        if (enemyData.enemyName == EnemyName.MistKnight)
        {
            enemyAISensor.OutOfRangeToAttackAction += enemyMouvement.ChasePlayer;
            enemyAISensor.InRangeToAttackAction += enemyMouvement.StopMoving;
        }
        AudioManager.GetInstance().PlaySound(clip);

        if (RNG.Instance.FloatRNG(0, 1) < expDropRate)
        {
            DropExpFlame();
        }
        gameObject.SetActive(false);
    }

    private void DropExpFlame()
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

    private void ManageMouvement()
    {
        float distanceWithTarget = ((Vector2)transform.position - enemyMouvement.Target).magnitude;
        if (distanceWithTarget > 0.5)
        {
            animationManager.ChangeAnimationState(enemyData.enemyName, AnimationState.Walk);
            enemyMouvement.ChasePlayer();
        }
        else if (distanceWithTarget <= 0.5)
        {
            animationManager.ChangeAnimationState(enemyData.enemyName, AnimationState.Idle);
            enemyMouvement.StopMoving();
        }
    }

    private void GetComponentsOnEnable()
    {
        enemyMouvement = GetComponent<EnemyMouvement>();
        animationManager = GetComponent<AnimationManager>();
    }
    #endregion
}
