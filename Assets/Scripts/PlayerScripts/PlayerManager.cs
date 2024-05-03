using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance => instance;

    int exp;
    int level;
    float xPosition;
    float yPosition;

    public float XPosition { get => xPosition; }
    public float YPosition { get => yPosition; }
    public int Exp { get => exp; set => exp = value; }
    public int Level { get => level; set => level = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();
    }

    private void GetPlayerPosition()
    {
        xPosition = gameObject.transform.localPosition.x;
        yPosition = gameObject.transform.localPosition.y;
    }

    public void Reset()
    {
        Exp = 0;
        Level = 0;
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
