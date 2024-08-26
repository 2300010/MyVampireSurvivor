using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    EnemyAISensor enemyAISensor;

    Vector2 target;
    [SerializeField] float baseSpeed;
    private float speed;
    private bool facingRight = false;

    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float Speed { get => speed; set => speed = value; }

    private void OnEnable()
    {
        if (gameObject.name == "MistKnightPrefab")
        {
            enemyAISensor = GetComponent<EnemyAISensor>();
            enemyAISensor.OutOfRangeToAttackAction += ChasePlayer;
        }
        else
        {
            ChasePlayer();
        }
    }

    private void Start()
    {
        SetTargetDestination();
    }

    private void SetTargetDestination()
    {
        target = PlayerMouvement.Instance().transform.position;
    }

    private void ChasePlayer()
    {
        if (target != null)
        {
            Speed = BaseSpeed;
            Vector2 movementDirection = (target - (Vector2)transform.position).normalized;

            if ((movementDirection.x < 0 && facingRight) || (movementDirection.x > 0 && !facingRight))
            {
                FlipCharacter();
            }

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }

}
