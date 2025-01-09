using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateboardController : MonoBehaviour
{
    public float acceleration = 10f;  // Rate of acceleration
    public float maxSpeed = 10f;      // Maximum speed
    public float jumpForce = 5f;      // Force for jumping
    public float groundCheckDistance = 0.6f; // Raycast distance to detect the ground
    public LayerMask groundLayer;     // LayerMask for ground detection

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent flipping over due to uneven surfaces
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
        ApplyFriction();
    }

    void HandleInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A and D keys
        float moveVertical = Input.GetAxis("Vertical");     // W and S keys

        // Calculate movement direction
        moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;
        moveDirection.Normalize();

        // Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void CheckGround()
    {
        // Check if the skateboard is grounded using a raycast
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, groundCheckDistance, groundLayer);
    }

    void ApplyMovement()
    {
        if (moveDirection.magnitude > 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection * acceleration, ForceMode.Acceleration);
        }
    }

    void ApplyFriction()
    {
        // Apply friction if no input is given
        if (moveDirection.magnitude < 0.1f && rb.velocity.magnitude > 0.1f)
        {
            rb.velocity *= 0.95f; // Friction multiplier
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Ensure consistent ground detection
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }
}
