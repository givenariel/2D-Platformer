using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private float playerMoveSpeed = 6f;
    [SerializeField] private float jumpForce = 6f;
    public int jumpCount;
    public Animator Animator { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }

    protected override void Start()
    {
        base.Start();
        Animator = GetComponent<Animator>();
        StateMachine = new PlayerStateMachine();
        StateMachine.ChangeState(new IdleState(), this);
        inputHandler = GetComponent<PlayerInputHandler>();
        moveSpeed = playerMoveSpeed;
    }

    public void SetAnimation(string animationName)
    {
        if (Animator != null)
        {
            Animator.Play(animationName);
        }
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    public void Jump()
    {
        rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }
}
