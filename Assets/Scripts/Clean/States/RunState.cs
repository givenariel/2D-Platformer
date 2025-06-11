using UnityEngine;

public class RunState : IPlayerState
{
    private Player player;

    public void Enter(Player p)
    {
        player = p;
        player.SetAnimation("run");
    }
    public void Exit() { }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        float moveInput = player.inputHandler.moveInput.x;
        player.Move(moveInput);

        if (player.inputHandler.moveInput.x == 0)
        {
            player.StateMachine.ChangeState(new IdleState(), player);
        } 
        
        if (player.inputHandler.jumpPressed == true)
        {
            player.StateMachine.ChangeState(new JumpState(), player);
        }
    }
}
