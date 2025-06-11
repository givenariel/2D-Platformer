using UnityEngine;
using UnityEngine.Animations;

public class IdleState : IPlayerState
{
    private Player player;

    public void Enter(Player p)
    {
        player = p;
        player.SetAnimation("idle");
    }   

    public void Exit() { }

    public void Update() { }

    public void FixedUpdate()
    {
        if (player.inputHandler.moveInput.x != 0)
        {
            player.StateMachine.ChangeState(new RunState(), player);
        }

        if (player.inputHandler.jumpPressed == true)
        {
            player.StateMachine.ChangeState(new JumpState(), player);
        }

    }
}
