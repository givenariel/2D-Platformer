using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f,
    jumpForce = 6f,
    groundDetectLength = 1.5f,
    sprintSpeed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int jumpCount;
    [SerializeField] private Transform groundDetect;
    [SerializeField] private int playerHP = 4;
    private RaycastHit2D hit;
    float horizontalInput, verticalInput;
    private bool climbable;
    IA_Player inputActions;

    private void OnEnable()
    {
        inputActions = new IA_Player();
        inputActions.Player.Movement.Enable();
        inputActions.Player.Sprint.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += Jump;
    }

    void OnDisable()
    {
        inputActions.Player.Movement.Disable();
        inputActions.Player.Sprint.Disable();
        inputActions.Player.Jump.Disable();
        inputActions.Player.Jump.performed -= Jump;
    }

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
        Movement();
    }

    private void Movement()
    {
        horizontalInput = inputActions.Player.Movement.ReadValue<Vector2>().x;
        verticalInput = inputActions.Player.Movement.ReadValue<Vector2>().y;
        if (!isSprinting())
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocityY);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontalInput * sprintSpeed, rb.linearVelocityY);
        }

        if (climbable)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, verticalInput * sprintSpeed);
        }

    }

    private bool isSprinting()
    {
        float sprintInput = inputActions.Player.Sprint.ReadValue<float>();
        if (sprintInput > 0 && Grounded())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && jumpCount < 2)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {
            playerHP -= 1;
        }
        if (collision.collider.CompareTag("healing"))
        {
            playerHP += 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            climbable = true;
        }
        else
        {
            climbable = false;
        }
    }
}
