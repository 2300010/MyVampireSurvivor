using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool facingRight = true;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float horizontalMove = horizontalInput * speed * Time.deltaTime;
        float verticalMove = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        body.velocity = new Vector2(horizontalMove, verticalMove);

        if(horizontalInput > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if(horizontalInput < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        
        Vector3 scale = transform.localScale;

        scale.x *= -1;
        transform.localScale = scale;
    }
}
