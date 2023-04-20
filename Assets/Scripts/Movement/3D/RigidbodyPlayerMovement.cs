using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private const float GROUND_CHECK_RADIUS = 0.1f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(float horizontal, float vertical, bool jump)
    {
        if (horizontal != 0f || vertical != 0f)
        {
            var movement = new Vector3(horizontal, 0.0f, vertical).normalized * moveSpeed;
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }

        if (jump && Physics.CheckSphere(groundCheck.position, GROUND_CHECK_RADIUS, groundMask))
        {
            Debug.Log("jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
