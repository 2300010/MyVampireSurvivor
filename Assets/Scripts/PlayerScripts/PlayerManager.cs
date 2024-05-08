using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance() => instance;

    int exp;
    int level;
    public int Exp { get => exp; set => exp = value; }
    public int Level { get => level; set => level = value; }

    HpManager playerHpManager;

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
        playerHpManager = GetComponent<HpManager>();
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

}
