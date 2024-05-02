using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour, Ipoolable
{
    //Declared variables for stats
    [SerializeField] float baseLifetime;
    [SerializeField] int baseWeaponDamage;
    [SerializeField] float baseSpeed;
    float currentLifetime;
    int currentWeaponDamage;
    float currentSpeed;
    Vector3 direction;
    

    public Vector3 Direction { get => direction; set => direction = value; }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        LifetimeManager();
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision with " + collision.gameObject.name);

        Collider2D collider = collision.collider;

        if (!collider.CompareTag("Player"))
        {
            DealDamage(collider.GameObject(), currentWeaponDamage);
            //Debug.Log("Recieved damage = " + currentWeaponDamage);
            gameObject.SetActive(false);
        }
    }

    private void DealDamage(GameObject opponent, int damage)
    {
        HpManager opponentHpManager = opponent.GetComponent<HpManager>();
        opponentHpManager.TakeDamage(damage);
    }

    private void LifetimeManager()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Movement()
    {
        transform.position += transform.right * currentSpeed * Time.deltaTime;
    }

    public void Reset()
    {
        currentLifetime = baseLifetime;
        currentSpeed = baseSpeed;
        currentWeaponDamage = baseWeaponDamage;
    }
}
