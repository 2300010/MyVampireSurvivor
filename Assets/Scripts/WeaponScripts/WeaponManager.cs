using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour, Ipoolable
{
    #region Variables
    //Declared variables for stats
    [SerializeField] float baseLifetime;
    [SerializeField] int baseDamage;
    [SerializeField] float baseSpeed;
    float currentLifetime;
    int currentDamage;
    float currentSpeed;
    Vector3 direction;
    #endregion

    public Vector3 Direction { get => direction; set => direction = value; }

    #region Unity functions

    private void OnEnable()
    {
        Reset();
        //Debug.Log("Current damage = " + currentDamage);
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
            DealDamage(collider.GameObject(), currentDamage);
            //Debug.Log("Recieved damage = " + currentDamage);
            gameObject.SetActive(false);
            ObjectPoolingSystem.Instance().ReturnPoolObject(gameObject);
        }
    }

    public void Reset()
    {
        currentLifetime = baseLifetime;
        currentSpeed = baseSpeed;
        currentDamage = GameManager.Instance.ScytheStatBlock.damage;
        //GameManager.LevelUp += LevelUpStatUpdate;
    }

    private void OnDestroy()
    {
        //GameManager.LevelUp -= LevelUpStatUpdate;
    }
    #endregion

    #region Custom functions
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
            currentLifetime = baseLifetime;
            ObjectPoolingSystem.Instance().ReturnPoolObject(gameObject);
        }
    }

    private void Movement()
    {
        transform.position += transform.right * currentSpeed * Time.deltaTime;
    }

    //To remove??

    //private void SetBaseStats()
    //{
    //    baseLifetime = GameManager.Instance.ScytheStatBlock.lifetime;
    //    baseSpeed = GameManager.Instance.ScytheStatBlock.speed;
    //    baseDamage = GameManager.Instance.ScytheStatBlock.damage;
    //}
    #endregion

}
