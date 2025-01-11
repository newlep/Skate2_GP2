using UnityEngine;

public class SkateboardMovement : MonoBehaviour
{
    public float acceleration = 10f; // Acceleration speed
    public float deceleration = 15f; // Deceleration speed
    public float maxSpeed = 20f;     // Maximum speed
    public float turnSpeed = 50f;    // Turning speed
    public float jumpForce = 5f;     // Force applied for jumping

    private Rigidbody rb;
    private bool isGrounded = true; // Check if the skateboard is on the ground
    private bool hasJumped = false; // Prevent double jumps while in the air

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is required for movement!");
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleTurning();
    }

    void Update()
    {
        HandleJump();
    }

    private void HandleMovement()
    {
        // Forward and backward movement
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (rb.velocity.magnitude > 0.1f) // Smooth deceleration
            {
                rb.AddForce(-rb.velocity.normalized * deceleration, ForceMode.Acceleration);
            }
            else
            {
                rb.velocity = Vector3.zero; // Stop completely
            }
        }

        // Clamp the maximum speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void HandleTurning()
    {
        // Turning left or right
        float turnInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            turnInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnInput = 1f;
        }

        if (turnInput != 0)
        {
            Quaternion rotation = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }

    private void HandleJump()
    {
        // Allow jump only if the player is moving and grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rb.velocity.magnitude > 0.1f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Player is no longer grounded
            hasJumped = true;   // Track the jump
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset grounded state when touching the ground
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f) // Check if collision is from below
            {
                isGrounded = true; // Allow jumping again
                hasJumped = false; // Reset jump flag
                break;
            }
        }
    }
}
