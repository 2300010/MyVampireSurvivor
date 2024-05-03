using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance => instance;

    int exp;
    float xPosition;
    float yPosition;

    public float XPosition { get => xPosition; }
    public float YPosition { get => yPosition; }
    public int Exp { get => exp; set => exp = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HpManager.PlayerDeath += OnDeath;
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

    private void OnDeath()
    {
        HpManager.PlayerDeath -= OnDeath;
        gameObject.SetActive(false);
    }
}
