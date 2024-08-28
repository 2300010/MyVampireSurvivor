using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    private Vector2 target;
    [SerializeField] float baseSpeed;
    private float speed;
    private bool facingRight = false;

    

    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float Speed { get => speed; set => speed = value; }
    public Vector2 Target { get => target; set => target = value; }

    private void SetTargetDestination()
    {
        Target = PlayerMouvement.Instance().transform.position;
    }

    public void ChasePlayer()
    {
        if (Target != null)
        {
            speed = BaseSpeed;
            Vector2 movementDirection = (Target - (Vector2)transform.position).normalized;

            if ((movementDirection.x < 0 && facingRight) || (movementDirection.x > 0 && !facingRight))
            {
                FlipCharacter();
            }

            transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        }
        SetTargetDestination();
    }

    public void StopMoving()
    {
        speed = 0;
        SetTargetDestination();
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }

}
