using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float lifetime;
    float currentLifetime = 0;
    Vector2 target;
    Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(SetTargetDirection() * speed);
    }

    private void Update()
    {
        currentLifetime += Time.deltaTime;
        if(currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;

        if (otherCollider.CompareTag("Player"))
        {
            HpManager playerHpManager = otherCollider.GetComponent<HpManager>();
            playerHpManager.TakeDamage(damage);
            //Debug.Log("Damage dealt = " + damage);
            Destroy(gameObject);
        }
    }

    private Vector2 SetTargetDirection()
    {
        target = PlayerMouvement.Instance().transform.position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        return direction;
    }
}
