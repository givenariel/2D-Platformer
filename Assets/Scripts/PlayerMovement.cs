using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpForce, groundDetectlength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundDetect;
    [SerializeField] private LayerMask groundLayer;
    private float moveInput;
    private Vector2 inputValue;
    private RaycastHit2D hit;
    IA_Player inputActions;

    private void Start()
    {
        inputActions = new IA_Player();
        inputActions.Player.Movement.Enable();
        inputActions.Player.Jump.Enable();
    }

    private void Update()
    {
        InputHandler();
        Movement();
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

    void Jump()
    {
        rb.AddForceY(jumpForce);
    }

    private bool Grounded()
    {
        hit = Physics2D.Raycast(gameObject.transform.position, -gameObject.transform.up, groundDetectlength, groundLayer);
        Debug.DrawRay(groundDetect.position, -groundDetect.up, Color.green);
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