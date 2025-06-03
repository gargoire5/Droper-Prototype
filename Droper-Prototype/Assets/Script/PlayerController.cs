using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputActionReference MoveAction;
    public InputActionReference LookAction;
    public InputActionReference JumpAction;

    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;

    public Rigidbody rb;
    public Transform playerCamera;

    private Vector2 inputDirection;
    private Vector2 lookDelta;
    private float verticalLookRotation = 0f;

    private bool isGrounded;

    

    void Start()
    {
        MoveAction.action.Enable();
        LookAction.action.Enable();
        JumpAction.action.Enable();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        inputDirection = MoveAction.action.ReadValue<Vector2>();
        lookDelta = LookAction.action.ReadValue<Vector2>();

        RotateView();

        if (JumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 move = transform.forward * inputDirection.y + transform.right * inputDirection.x;
        Vector3 velocity = move * speed;
        Vector3 rbVelocity = new Vector3(velocity.x, velocity.y, velocity.z);
        rb.velocity = rbVelocity;
    }

    private void RotateView()
    {
        Vector2 mouseDelta = lookDelta * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseDelta.x);

        verticalLookRotation -= mouseDelta.y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts) 
        {
            if(Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    
}


