using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject expPrefab;
    float dropRate = 0.5f;
    int playerExp;

    public int PlayerExp { get => playerExp; set => playerExp = value; }
    public float DropRate { get => dropRate; set => dropRate = value; }

    private void Start()
    {
        HpManager.EnemyDeath += OnEnemyDeath;
        ExpFlameManager.OnExpPickup += PlayerReceiveExp;
    }

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
        //Debug.LogWarning("Exp received = " + expReceived + " Player exp = " + playerExp);
    }
}
