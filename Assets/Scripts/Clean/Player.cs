using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private float playerMoveSpeed = 6f;
    private float moveInput;
    PlayerInputHandler inputHandler;
    protected override void Start()
    {
        base.Start();
        inputHandler = GetComponent<PlayerInputHandler>();
        moveSpeed = playerMoveSpeed;
    }
    void FixedUpdate()
    {
        moveInput = inputHandler.moveInput.x;
        Move(moveInput);
    }
}
