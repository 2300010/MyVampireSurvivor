using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    Vector2 target;
    [SerializeField] float baseSpeed;
    private float speed;

    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
    public float Speed { get => speed; set => speed = value; }

    // Update is called once per frame
    void Update()
    {
        SetTargetDestination();
        ChasePlayer();
    }

    private void SetTargetDestination()
    {
        target = new Vector2(PlayerManager.Instance.XPosition, PlayerManager.Instance.YPosition);
    }

    private void ChasePlayer()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
