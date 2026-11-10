using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("look")]
    [SerializeField] private InputActionReference lookReference;

    [SerializeField] private float sentivity = 0.1f;
    [SerializeField] private float pitchLimit = 89f;
    [SerializeField] private bool inversY = false;

    private float yaw;
    private float pitch;
    private Vector2 lookInput;

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void ReadLookInput()
    {
        lookInput = lookReference.action.ReadValue<Vector2>();
        Debug.Log(lookInput);
    }

    private void ApplyRotation()
    {
        float sing = inversY ? 1f : -1f;

        yaw += lookInput.x * sentivity;
        pitch += lookInput.y * sentivity * sing;

        pitch = Mathf.Clamp(pitch, -pitchLimit, pitchLimit);

        transform.localEulerAngles = new Vector3(pitch, yaw, 0f);
    }

    private void OnEnable()
    {
        lookReference.action.Enable();
    }

    private void Update()
    {
        ReadLookInput();
        ApplyRotation();
    }

    private void OnDisable()
    {
        lookReference.action.Disable();
    }
}
