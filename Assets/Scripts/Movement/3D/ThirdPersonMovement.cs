using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour, IMovement
{
    [SerializeField] private Transform camera;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private CharacterController controller;
    private float turnSmoothVelocity = 0f;
    private float verticalVelocity = 0f;

    private const float gravity = -9.81f;
    private const float turnSmoothTime = 0.1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Move(float horizontal, float vertical, bool jump)
    {
        var direction = new Vector3(horizontal, 0f, vertical);
        var movement = Vector3.zero;

        if(direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
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
