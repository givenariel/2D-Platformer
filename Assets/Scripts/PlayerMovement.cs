using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpForce, groundDetectlength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int jumpCount;
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
        if (jumpCount < 1)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private bool Grounded()
    {
        hit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.up, groundDetectlength, groundLayer);
        Debug.DrawRay(gameObject.transform.position, -gameObject.transform.up, Color.green);
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
}