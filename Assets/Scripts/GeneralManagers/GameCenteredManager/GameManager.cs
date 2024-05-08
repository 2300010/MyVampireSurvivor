using System;
using UnityEngine;

public class WeaponStatBlock
{
    public int damage;
    public float speed;
    public float lifetime;
    //public float range;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;

    #region Variables
    //Declare weapon stats variables
    WeaponStatBlock scytheStatBlock;

    //Declare exp variables
    [SerializeField] GameObject expPrefab;
    float dropRate = 0.5f;
    
    //Declare player exp stats variables
    int playerExp = 0;
    int totalPlayerExp;
    int playerLevel = 1;
    int expForLevel = 20;
    #endregion

    #region Actions and Events
    public static Action LevelUp;
    public static Action ExpGained;
    #endregion

    #region Getters and Setters
    public WeaponStatBlock ScytheStatBlock { get => scytheStatBlock; set => scytheStatBlock = value; }
    public float DropRate { get => dropRate; set => dropRate = value; }
    public int PlayerExp { get => playerExp; set => playerExp = value; }
    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }
    public int TotalPlayerExp { get => totalPlayerExp; set => totalPlayerExp = value; }
    public int ExpForLevel { get => expForLevel; set => expForLevel = value; }
    #endregion

    #region Unity functions
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
        scytheStatBlock = new WeaponStatBlock();
        scytheStatBlock.damage = 2;
        HpManager.EnemyDeath += OnEnemyDeath;
        ExpFlameManager.OnExpPickup += PlayerReceiveExp;
    }

    private void OnDestroy()
    {
        HpManager.EnemyDeath -= OnEnemyDeath;
        ExpFlameManager.OnExpPickup -= PlayerReceiveExp;
    }
    #endregion

    //private void OnStateUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        playerLevel++;
    //        scytheStatBlock.damage += 2;
    //        LevelUp?.Invoke();
    //    }
    //}

    #region Custom functions
    private void OnEnemyDeath(Vector2 deathPosition, int expDropped)
    {
        if (RNG.Instance.FloatRNG(0, 1) < DropRate)
        {
            ExpFlameDrop(deathPosition, expDropped);
        }
    }

    private void ExpFlameDrop(Vector2 position, int expGiven)
    {
        if (expPrefab != null)
        {
            GameObject expFlame = Instantiate(expPrefab, position, Quaternion.identity);
            
            ExpFlameManager flameManager = expFlame.GetComponent<ExpFlameManager>();
            if(flameManager != null)
            {
                flameManager.ExpGiven = expGiven;
                //Debug.Log("Exp given = " + expGiven);
            }
        }
    }

    private void PlayerReceiveExp(int expReceived)
    {
        playerExp += expReceived;
        TotalPlayerExp += expReceived;


        //Debug.LogWarning("Exp received = " + expReceived + " Player exp = " + playerExp);
        //Debug.LogWarning("Total exp = " + totalPlayerExp);

        if(playerExp >= expForLevel)
        {
            ExpManageOnLevelUp();
            StatsManageOnLevelUp();
            LevelUp?.Invoke();
            //Debug.Log("Level = " + playerLevel);
        }

        ExpGained?.Invoke();
    }

    private void ExpManageOnLevelUp()
    {
        playerLevel++;
        playerExp -= expForLevel;
        expForLevel = expForLevel * 3 / 2;
    }

    private void StatsManageOnLevelUp()
    {
        scytheStatBlock.damage += 2;
    }
    #endregion
}
