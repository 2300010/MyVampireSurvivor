using UnityEngine;

public enum CharacterType
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
        playerHpManager.CurrentHp = playerHpManager.MaxHp;
        HpManager.PlayerDeath += OnDeath;
        GameManager.LevelUp += UpdateStats;
        animationManager = GetComponent<AnimationManager>();
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
        GameManager.LevelUp -= UpdateStats;
        gameObject.SetActive(false);
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

        float horizontalMove = horizontalInput * speed * Time.deltaTime;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        body.velocity = new Vector2(horizontalMove, verticalMove);

        if (VelocityIsZero())
        {
            animationManager.ChangeAnimationState("Wizard_Idle");
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
            animationManager.ChangeAnimationState("Wizard_Walking");
        }
    }
    #endregion
}
