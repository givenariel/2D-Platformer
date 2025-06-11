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
    }

    private void OnEnable()
    {
        inputActions.Player.Movement.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Movement.Disable();
    }
}
