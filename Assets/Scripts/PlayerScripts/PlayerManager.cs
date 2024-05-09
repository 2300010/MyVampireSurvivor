using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance() => instance;

    //TO REMOVE LATER!!!!
    [SerializeField] GameObject weapon;

    [SerializeField] HpManager playerHpManager;
    [SerializeField] GameObject spawnPoint;
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
        InputManager.SpaceBarPressed += Attack;
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
        WeaponManager weaponManager = weapon.GetComponent<WeaponManager>();
        int instancesToSpawn = weaponManager.NumberOfInstances;

        for (int i = 0; i < instancesToSpawn; i++)
        {
            if (weapon != null)
            {
                Quaternion rotation;

                if (i == 0)
                {
                    weapon.transform.SetPositionAndRotation(spawnPoint.transform.position, Quaternion.identity);
                }
                else if (i == 1)
                {
                    rotation = Quaternion.Euler(0f, 0f, 15f);
                    weapon.transform.SetPositionAndRotation(spawnPoint.transform.position, rotation);
                }
                else if (i == 2)
                {
                    rotation = Quaternion.Euler(0f, 0f, 345f);
                    weapon.transform.SetPositionAndRotation(spawnPoint.transform.position, rotation);
                }

                Instantiate(weapon, spawnPoint.transform.position, Quaternion.identity);
                //weapon.SetActive(true);
            }
            else
            {
                break;
            }
        }
    }
    #endregion
}
