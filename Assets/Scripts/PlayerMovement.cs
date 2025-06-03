using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpForce, groundDetectlength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int jumpCount;
    private float moveInput;
    private Vector2 inputValue;
    private RaycastHit2D hit;
    IA_Player inputActions;

    private void Start()
    {
        inputActions = new IA_Player();
        inputActions.Player.Movement.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        InputHandler();
        Movement();
        
        if (Grounded())
        {
            jumpCount = 0;
        }
    }

    void InputHandler()
    {
        inputValue = inputActions.Player.Movement.ReadValue<Vector2>();
        moveInput = inputValue.x;
    }

    void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpCount < 1)
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
}