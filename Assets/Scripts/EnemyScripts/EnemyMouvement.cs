using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    Vector2 target;
    [SerializeField] float baseSpeed;
    private float speed;
    private bool facingRight = false;

    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float Speed { get => speed; set => speed = value; }

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            EnemyAISensor tempSensor = child.GetComponent<EnemyAISensor>();
            if (tempSensor.RangeCollider)
            {
                tempSensor.OnTriggerEnterAction += StopMoving;
                tempSensor.OnTriggerExitAction += ChasePlayer;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetTargetDestination();
        ChasePlayer();
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

    private void StopMoving()
    {
        Speed = 0;
    }

}
