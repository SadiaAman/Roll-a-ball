using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded = true;
    private bool jumpPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle jump input in Update for better responsiveness
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveVertical = 1f;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveVertical = -1f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveHorizontal = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveHorizontal = 1f;

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.AddForce(movement * speed);

        // Handle jump in FixedUpdate for physics consistency
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpPressed = false; // Reset the jump flag
        }
        else if (jumpPressed)
        {
            jumpPressed = false; // Reset the flag even if we can't jump
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has landed on the ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.name.ToLower().Contains("plane") || collision.gameObject.name.ToLower().Contains("ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Keep grounded while touching ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.name.ToLower().Contains("plane") || collision.gameObject.name.ToLower().Contains("ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Only set not grounded if we're leaving the ground and have upward velocity
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.name.ToLower().Contains("plane") || collision.gameObject.name.ToLower().Contains("ground"))
            && rb.linearVelocity.y > 0.1f)
        {
            isGrounded = false;
        }
    }
}