using System;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    private static PlayerMouvement instance;
    
    public static PlayerMouvement Instance() => instance;

    [SerializeField] private float speed;
    private Rigidbody2D body;
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

    // OnStateUpdate is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float horizontalMove = horizontalInput * speed * Time.deltaTime;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        body.velocity = new Vector2(horizontalMove, verticalMove);

        if(horizontalInput > 0 && !FacingRight)
        {
            FlipCharacter();
        }
        else if(horizontalInput < 0 && FacingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        FacingRight = !FacingRight;
        
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }

}
