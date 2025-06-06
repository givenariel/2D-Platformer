using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f,
    jumpForce = 6f,
    groundDetectLength = 1.5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int jumpCount;
    [SerializeField] private Transform groundDetect;
    private Vector2 moveInput;
    private RaycastHit2D hit;
    float horizontalInput;

    private void Update()
    {
        Grounded();
        Flip();

        if (Grounded())
        {
            jumpCount = 0;
        }
        // if (!Grounded())
        // {
        //     rb.gravityScale = 7;
        // }
        // else
        // {
        //     rb.gravityScale = 1;
        // }
    }

    void FixedUpdate()
    {
        horizontalInput = moveInput.x;
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocityY);
    }

    private void OnMovement(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    private void OnJump()
    {
        if (jumpCount < 2)
        {
            rb.AddForceY(jumpForce);
            jumpCount++;
        }
    }

    private bool Grounded()
    {
        hit = Physics2D.Raycast(groundDetect.position, -groundDetect.up, groundDetectLength, groundLayer);
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Flip()
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(groundDetect.position, -groundDetect.up, Color.red);
    }
}
