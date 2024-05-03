using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject expPrefab;
    float dropRate = 0.5f;

    private void Start()
    {
        HpManager.EnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(Vector2 deathPosition, int expDropped)
    {
        if (RNG.Instance.FloatRNG(0, 1) < dropRate)
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
                Debug.Log("Exp given = " + expGiven);
            }
        }
    }
}
