using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private IA_Player playerControls;
    private Rigidbody2D rb;

    void Awake()
    {
        playerControls = new IA_Player();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Jump.performed += Jump;
    }

    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump");
            rb.AddForceY(50f);
        }
    }
}
