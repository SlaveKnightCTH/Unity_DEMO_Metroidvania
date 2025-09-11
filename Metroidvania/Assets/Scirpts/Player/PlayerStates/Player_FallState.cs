using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string aniBoolName) : base(player, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Debug.Log("enter the fallstate");
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
