using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private CharacterController controller;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 1.2f;

    private PlayerInputReader input;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool wantJump;
    [SerializeField] private float turnSpeed = 12f;

    void Awake()
    {

        controller = GetComponent<CharacterController>();
        input = new PlayerInputReader();

        jumpAction = input.Player.Jump;
        moveAction = input.Player.Move;
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        Vector3 direction = GetCameraRelativeDirection(moveInput).normalized;

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (jumpAction.WasPressedThisFrame() && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = new Vector3(direction.x * moveSpeed, verticalVelocity, direction.z * moveSpeed);

        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 GetCameraRelativeDirection(Vector2 input)
    {
        return Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f) * new Vector3(moveInput.x, 0f, moveInput.y);
    }
}
