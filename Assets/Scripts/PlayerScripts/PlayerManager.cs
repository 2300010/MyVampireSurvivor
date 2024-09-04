using UnityEngine;

public enum CharacterName
{
    Wizard,
    Warrior,
    Archer,
    Thief,
    Pirate
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] CharacterData characterData;

    private static PlayerManager instance;
    public static PlayerManager Instance() => instance;

    [SerializeField] HpManager playerHpManager;
    int exp;
    int level;
    private AnimationManager animationManager;
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool facingRight = true;

    public bool FacingRight { get => facingRight; set => facingRight = value; }

    public int Exp { get => exp; set => exp = value; }
    public int Level { get => level; set => level = value; }

    #region Unity Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Reset();
        //StartCoroutine(Attack());
    }

    public void Reset()
    {
        Exp = 0;
        Level = 0;
        //speed = characterData.baseSpeed;
        playerHpManager.CurrentHp = playerHpManager.MaxHp;
        HpManager.PlayerDeath += OnDeath;
        HpManager.PlayerIsTakingDamage += OnDamageTaken;
        GameManager.LevelUp += UpdateStats;
        animationManager = GetComponent<AnimationManager>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        ManageMouvement();
    }
    #endregion

    #region Custom Methods
    private void OnDeath()
    {
        HpManager.PlayerDeath -= OnDeath;
        HpManager.PlayerIsTakingDamage -= OnDamageTaken;
        GameManager.LevelUp -= UpdateStats;
        ManageAnimation(characterData, AnimationState.Death);
        gameObject.SetActive(false);
    }

    private void OnDamageTaken()
    {
        ManageAnimation(characterData, AnimationState.Hurt);
    }

    private void UpdateStats()
    {
        exp = GameManager.Instance.PlayerExp;
        level = GameManager.Instance.PlayerLevel;
    }

    private void Attack()
    {
        Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void ManageMouvement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        //Debug.Log("Horizontal input = " + horizontalInput);

        float horizontalMove = horizontalInput * speed * Time.deltaTime;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        body.velocity = new Vector2(horizontalMove, verticalMove);

        if (VelocityIsZero())
        {
            ManageAnimation(characterData, AnimationState.Idle);
        }
        else
        {
            if (horizontalInput > 0 && !FacingRight)
            {
                FlipCharacter();
            }
            else if (horizontalInput < 0 && FacingRight)
            {
                FlipCharacter();
            }
            ManageAnimation(characterData, AnimationState.Move);
        }
    }

    private bool VelocityIsZero()
    {
        if (body.velocity.sqrMagnitude > 0)
        {
            return false;
        }
        return true;
    }

    private void FlipCharacter()
    {
        FacingRight = !FacingRight;

        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }

    private void ManageAnimation(CharacterData currentCharacterData, AnimationState wantedAnimationState)
    {
        animationManager.ChangeAnimationState(currentCharacterData.characterName, wantedAnimationState);
    }
    #endregion
}
