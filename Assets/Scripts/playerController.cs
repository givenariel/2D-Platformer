using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f,
    jumpForce = 6f,
    groundDetectLength = 1.5f,
    wallDetectLength = 1.5f,
    sprintSpeed = 20f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer,
    wallLayer;
    [SerializeField] private int jumpCount;
    [SerializeField] private Transform groundDetect,
    wallDetect;
    [SerializeField] private GameObject weapon;
    [SerializeField] private int playerHP = 4;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    private RaycastHit2D hit;
    float horizontalInput, verticalInput;
    [SerializeField] private bool climbable, onWall;
    IA_Player inputActions;

    private void OnEnable()
    {
        inputActions = new IA_Player();
        inputActions.Player.Movement.Enable();
        inputActions.Player.Sprint.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Melee.Enable();
        inputActions.Player.Melee.performed += Melee;
        inputActions.Player.Melee.canceled += Melee;
        inputActions.Player.Shooting.Enable();
        inputActions.Player.Shooting.performed += Shoot;
    }

    void OnDisable()
    {
        inputActions.Player.Movement.Disable();
        inputActions.Player.Sprint.Disable();
        inputActions.Player.Jump.Disable();
        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Melee.Disable();
        inputActions.Player.Melee.performed -= Melee;
        inputActions.Player.Shooting.Enable();
        inputActions.Player.Shooting.performed -= Shoot;
    }

    private void Update()
    {
        Grounded();
        WallDetect();
        wallSlide();
        Flip();

        if (Grounded())
        {
            jumpCount = 0;
        }

        if (WallDetect())
        {
            onWall = true;
        }
        else
        {
            onWall = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && WallDetect() && !Grounded())
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
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

    private bool WallDetect()
    {
        hit = Physics2D.Raycast(wallDetect.position, wallDetect.right, wallDetectLength, wallLayer);
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void wallSlide()
    {
        if (WallDetect() & !Grounded())
        {
            // rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * -0.15f);
            rb.linearVelocityY = -1f;
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

    private void Melee(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            weapon.SetActive(true);
        }
        else if (ctx.canceled)
        {
            weapon.SetActive(false);
        }
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        }
    }
}
