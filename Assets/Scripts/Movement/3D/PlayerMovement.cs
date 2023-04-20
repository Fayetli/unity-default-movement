using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IMovement
{
    private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private Vector3 movement;
    private const float gravity = -9.81f;
    private float verticalVelocity = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(float horizontal, float vertical, bool jump)
    {
        movement = new Vector3(horizontal, 0f, vertical);
        movement *= speed;

        if (controller.isGrounded)
        {
            if (jump)
            {
                verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -gravity);
            }
            else
            {
                verticalVelocity = 0f;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime * 1.02f;
        }
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);
    }
}
