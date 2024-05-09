using UnityEngine;

public class MistKnightManager : MonoBehaviour
{
    [SerializeField] EnemyAISensor enemyAISensor;
    [SerializeField] EnemyMouvement enemyMouvement;
    [SerializeField] GameObject projectile;
    [SerializeField] float attackCooldownTime;
    float timer = 0;

    private void Start()
    {
        enemyAISensor.InRangeToAttackAction += RangedAttack;
    }

    public void RangedAttack()
    {
        enemyMouvement.Speed = 0;
        //Debug.Log("Timer = " + timer);

        if (timer >= attackCooldownTime)
        {
            //Debug.Log("Enemy spawning");
            SpawnProjectile();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    private void SpawnProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
