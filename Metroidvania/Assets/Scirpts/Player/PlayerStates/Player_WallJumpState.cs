public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string aniBoolName) : base(player, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * player.facingDir * -1, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }


        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }


    }
}
