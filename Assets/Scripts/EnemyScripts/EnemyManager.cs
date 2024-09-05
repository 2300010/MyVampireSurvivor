using System.Collections;
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
    [SerializeField] float delayOnDamageTaken;
    private bool isDamaged;
    private bool isDead;

    EnemyMouvement enemyMouvement;
    AnimationManager animationManager;
    EnemyAISensor enemyAISensor;
    private float damageTimer;

    public int ExpDropped { get => expDropped; set => expDropped = value; }
    public EnemyData EnemyData { get => enemyData; }

    #region Unity Methods
    private void OnEnable()
    {
        SetComponentsOnEnable();
        if (gameObject.name == "MistKnightPrefab")
        {
            enemyAISensor = GetComponent<EnemyAISensor>();
            enemyAISensor.OutOfRangeToAttackAction += enemyMouvement.ChasePlayer;
            enemyAISensor.InRangeToAttackAction += enemyMouvement.StopMoving;
        }

        hpManager.CurrentHp = EnemyData.baseHp;
        isDamaged = false;
        isDead = false;
        damageTimer = 0f;


        //Debug.Log("HP = " + hpManager.CurrentHp + " Character = " + enemyData.enemyName);
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

        if (collider.CompareTag(ObjectTag.Player.ToString()))
        {
            DealDamage();
        }
    }
    #endregion

    #region Custom Method

    public void OnDeath()
    {
        isDead = true;
        StartCoroutine(DeathSequence());
    }

    public void OnDamageTaken()
    {
        if (!isDamaged)
        {
            enemyMouvement.StopMoving();

            ManageEnemyAnimation(AnimationState.Hurt);

            damageTimer = 0f;
            isDamaged = true;
        }
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
                flameManager.SetupOnEnable();
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

        if (isDamaged)
        {
            if (!isDead)
            {
                damageTimer += Time.fixedDeltaTime;

                if (damageTimer >= delayOnDamageTaken)
                {
                    if (distanceWithTarget > 0.5)
                    {
                        ManageEnemyAnimation(AnimationState.Move);
                        enemyMouvement.ChasePlayer();
                    }
                    else if (distanceWithTarget <= 0.5)
                    {
                        ManageEnemyAnimation(AnimationState.Idle);
                        enemyMouvement.StopMoving();
                    }
                    isDamaged = false;

                }
            }
            else
            {
                return;
            }
        }
        else
        {
            if (distanceWithTarget > 0.5)
            {
                ManageEnemyAnimation(AnimationState.Move);
                enemyMouvement.ChasePlayer();
            }
            else if (distanceWithTarget <= 0.5)
            {
                ManageEnemyAnimation(AnimationState.Idle);
                enemyMouvement.StopMoving();
            }
        }
    }

    private void SetComponentsOnEnable()
    {
        enemyMouvement = GetComponent<EnemyMouvement>();
        animationManager = GetComponent<AnimationManager>();
    }

    private void ManageEnemyAnimation(AnimationState wantedAnimationState)
    {
        animationManager.ChangeAnimationState(EnemyData.enemyName, wantedAnimationState);
    }

    IEnumerator DeathSequence()
    {
        //ManageEnemyAnimation(AnimationState.Hurt);

        Debug.Log("Playing Hurt animation!");

        yield return new WaitUntil(() => IsAnimationFinished(AnimationState.Hurt));

        yield return new WaitForSeconds(0.5f);


        AudioManager.GetInstance().PlaySound(clip);
        ManageEnemyAnimation(AnimationState.Death);

        yield return new WaitUntil(() => IsAnimationFinished(AnimationState.Death));

        if (EnemyData.enemyName == EnemyName.MistKnight)
        {
            enemyAISensor.OutOfRangeToAttackAction -= enemyMouvement.ChasePlayer;
            enemyAISensor.InRangeToAttackAction -= enemyMouvement.StopMoving;
        }

        gameObject.SetActive(false);

        if (RNG.Instance.FloatRNG(0, 1) < expDropRate)
        {
            DropExpFlame();
        }
    }

    private bool IsAnimationFinished(AnimationState animation)
    {
        AnimatorStateInfo stateInfo = animationManager.GetAnimationState();
        //Debug.Log($"Current State: {stateInfo.fullPathHash}, Expected: {Animator.StringToHash(animation.ToString())}, Normalized Time: {stateInfo.normalizedTime}");
        return stateInfo.IsName(EnemyData.enemyName.ToString() + "_" + animation.ToString()) && stateInfo.normalizedTime >= 1f;
    }
    #endregion
}
