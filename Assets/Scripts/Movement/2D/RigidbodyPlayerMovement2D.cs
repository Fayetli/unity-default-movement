using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyPlayerMovement2D : MonoBehaviour, IMovement2D
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private const float GROUND_CHECK_RADIUS = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontal, bool jumpDownBtn, bool jumpUpBtn)
    {
        if (jumpDownBtn && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

        if(jumpUpBtn && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        Flip(horizontal);
    }

    private bool IsGrounded()
        => Physics2D.OverlapCircle(groundCheck.position, GROUND_CHECK_RADIUS, groundLayer);

    private void Flip(float horizontal)
    {
        if(isFacingRight && horizontal < 0f || isFacingRight == false && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            var localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
