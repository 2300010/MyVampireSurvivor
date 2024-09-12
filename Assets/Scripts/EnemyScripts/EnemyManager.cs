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
    private bool isDead;

    EnemyMouvement enemyMouvement;
    AnimationManager animationManager;
    EnemyAISensor enemyAISensor;

    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;
    private Material enemyMaterial;

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
        isDead = false;
        enemyMaterial = GetComponent<SpriteRenderer>().material;

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
        StartCoroutine(DamageFlash());
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

        if (!isDead)
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
        else
        {
            return;
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

    private IEnumerator DeathSequence()
    {
        ManageEnemyAnimation(AnimationState.Hurt);

        //Debug.Log("Playing Hurt animation!");

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

    private IEnumerator DamageFlash()
    {
        enemyMaterial.SetColor("_FlashColor", flashColor);

        float currentFlashAmount = 0f;
        float elaspedTime = 0f;

        while (elaspedTime < flashTime)
        {
            elaspedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, 0f, (elaspedTime / flashTime));
            enemyMaterial.SetFloat("_FlashAmount", currentFlashAmount);

            yield return null;
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
