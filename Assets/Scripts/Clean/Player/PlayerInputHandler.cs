using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool sprintPressed { get; private set; }

    private IA_Player inputActions;

    private void Awake()
    {
        inputActions = new IA_Player();
        inputActions.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
        inputActions.Player.Jump.canceled += ctx => jumpPressed = false;
    }

    private void OnEnable()
    {
        inputActions.Player.Movement.Enable();
        inputActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Movement.Disable();
        inputActions.Player.Jump.Disable();
    }
}
