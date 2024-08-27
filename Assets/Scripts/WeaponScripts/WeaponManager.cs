using UnityEngine;

public class WeaponManager : MonoBehaviour, Ipoolable
{
    #region Variables
    //Declared variables for stats 
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float baseLifetime;
    [SerializeField] int baseDamage;
    [SerializeField] float baseSpeed;
    float currentLifetime;
    int currentDamage;
    float speed;
    Vector3 direction;
    #endregion


    #region Unity Methods

    private void OnEnable()
    {
        Reset();
        SetDirection();
        //Debug.Log("Current damage = " + currentDamage);
    }
    private void FixedUpdate()
    {
        LifetimeManager();
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision with " + collision.gameObject.name);

        Collider2D collider = collision.collider;

        if (!collider.CompareTag("Player"))
        {
            DealDamage(collider.gameObject, currentDamage);
            Debug.Log("Recieved damage = " + currentDamage);
            gameObject.SetActive(false);
            //ObjectPoolingSystem.Instance().ReturnPoolObject(gameObject);
        }
    }

    public void Reset()
    {
        currentLifetime = baseLifetime;
        currentDamage = baseDamage;
        speed = baseSpeed;
        //GameManager.LevelUp += LevelUpStatUpdate;
    }

    private void OnDestroy()
    {
        //GameManager.LevelUp -= LevelUpStatUpdate;
    }
    #endregion

    #region Custom Methods
    private void DealDamage(GameObject opponent, int damage)
    {
        HpManager opponentHpManager = opponent.GetComponent<HpManager>();
        opponentHpManager.TakeDamage(damage);
    }

    private void LifetimeManager()
    {
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            gameObject.SetActive(false);
            //currentLifetime = baseLifetime;
            //ObjectPoolingSystem.Instance().ReturnPoolObject(gameObject);
        }
    }

    private void SetDirection()
    {
        if (PlayerMouvement.Instance().FacingRight)
        {
            direction = transform.right;
        }
        else
        {
            FlipWeapon();
            direction = transform.right * -1;
        }
    }

    private void LevelUpStatUpdate()
    {
        baseDamage *= 3 / 2;
    }

    private void FlipWeapon()
    {
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }


    //To remove??

    //private void SetBaseStats()
    //{
    //    baseLifetime = GameManager.Instance.ScytheStatBlock.lifetime;
    //    baseSpeed = GameManager.Instance.ScytheStatBlock.baseSpeed;
    //    baseDamage = GameManager.Instance.ScytheStatBlock.damage;
    //}
    #endregion

}
