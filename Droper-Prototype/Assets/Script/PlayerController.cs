using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputActionReference MoveAction;
    public InputActionReference LookAction;
    public InputActionReference JumpAction;

    public float speed = 5f;
    public float mouseSensitivity = 2f;

    public Transform playerCamera;

    private CharacterController controller;
    private Vector2 inputDirection;
    private Vector2 lookDelta;
    private float verticalLookRotation = 0f;

    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;
    private bool isGrounded;

    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        MoveAction.action.Enable();
        LookAction.action.Enable();
        JumpAction.action.Enable();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleJump();
    }

    void HandleMovement()
    {
        inputDirection = MoveAction.action.ReadValue<Vector2>();
        Vector3 move = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleLook()
    {
        lookDelta = LookAction.action.ReadValue<Vector2>() * mouseSensitivity;

        transform.Rotate(Vector3.up * lookDelta.x);

        verticalLookRotation -= lookDelta.y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    void HandleJump()
    {
        isGrounded = controller.isGrounded;

        Debug.Log("isGrounded: " + controller.isGrounded);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (isGrounded && JumpAction.action.WasPressedThisFrame())
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (JumpAction.action.WasPressedThisFrame())
        {
            Debug.Log("Jump pressed!");
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 verticalMove = Vector3.up * verticalVelocity;
        controller.Move(verticalMove * Time.deltaTime);
    }

    
}


