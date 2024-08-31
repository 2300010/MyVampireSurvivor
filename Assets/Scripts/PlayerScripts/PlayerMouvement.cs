using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private static PlayerMouvement instance;

    public static PlayerMouvement Instance() => instance;

    [SerializeField] private float speed;
    private Rigidbody2D body;
    private AnimationManager animationManager;
    private bool facingRight = true;

    public bool FacingRight { get => facingRight; set => facingRight = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    } 

    private void FlipCharacter()
    {
        FacingRight = !FacingRight;

        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool VelocityIsZero()
    {
        if (body.velocity.sqrMagnitude > 0)
        {
            return false;
        }
        return true;
    }
}
