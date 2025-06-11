using UnityEngine;

public class JumpState : IPlayerState
{
    private Player player;

    public void Enter(Player p)
    {
        player = p;
        player.SetAnimation("jump");
    }   

    public void Exit() { }

    public void Update() { }

    public void FixedUpdate()
    {
        if (player.inputHandler.jumpPressed)
        {
            player.Jump();
            player.jumpCount++;
        }

        if (player.inputHandler.moveInput.x == 0 && player.inputHandler.jumpPressed == false)
        {
            player.StateMachine.ChangeState(new IdleState(), player);
        } 
        if (player.inputHandler.moveInput.x != 0 && player.inputHandler.jumpPressed == false)
        {
            player.StateMachine.ChangeState(new RunState(), player);
        } 
    }
}
