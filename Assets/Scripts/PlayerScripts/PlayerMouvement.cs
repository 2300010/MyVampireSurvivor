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
        animationManager = GetComponent<AnimationManager>();
        animationManager.ChangeAnimationState("Wizard_Idle");
    }

    // OnStateUpdate is called once per frame
    void FixedUpdate()
    {
        ManageMouvement();
    }

    private void ManageMouvement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float horizontalMove = horizontalInput * speed * Time.deltaTime;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        body.velocity = new Vector2(horizontalMove, verticalMove);

        if (VelocityIsZero())
        {
            animationManager.ChangeAnimationState("Wizard_Idle");
        }
        else
        {
            if (horizontalInput > 0 && !FacingRight)
            {
                FlipCharacter();
            }
            else if (horizontalInput < 0 && FacingRight)
            {
                FlipCharacter();
            }
            animationManager.ChangeAnimationState("Wizard_Walking");
        }
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
