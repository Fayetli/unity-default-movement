using UnityEngine;

class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private IMovement movement;

    private void Start()
    {
        movement = player.GetComponent<IMovement>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var jump = Input.GetButtonDown("Jump");
        movement.Move(horizontal, vertical, jump);
    }
}
