using UnityEngine;

public class PlayerStateMachine
{
    public IPlayerState CurrentState { get; private set; }

    public void ChangeState(IPlayerState newState, Player player)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter(player);
    }

    public void Update() => CurrentState?.Update();
    public void FixedUpdate() => CurrentState?.FixedUpdate();
}
