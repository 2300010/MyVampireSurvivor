using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance() => instance;

    [SerializeField] HpManager playerHpManager;
    int exp;
    int level;
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
    }

    public void Reset()
    {
        Exp = 0;
        Level = 0;
        playerHpManager.CurrentHp = playerHpManager.MaxHp;
        HpManager.PlayerDeath += OnDeath;
        GameManager.LevelUp += UpdateStats;
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
    #endregion
}
