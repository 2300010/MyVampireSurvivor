using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    Vector2 target;

    private void OnEnable()
    {
        target = PlayerMouvement.Instance.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;

        if (otherCollider.CompareTag("Player"))
        {
            HpManager playerHpManager = otherCollider.GetComponent<HpManager>();
            playerHpManager.TakeDamage(damage);
            Debug.Log("Damage dealt = " + damage);
            Destroy(gameObject);
        }
    }
}
