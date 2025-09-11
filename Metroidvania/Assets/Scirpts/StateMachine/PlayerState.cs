public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInput input;

    public PlayerState(Player player, StateMachine stateMachine, string aniBoolName) : base(stateMachine, aniBoolName)
    {
        this.player = player;

        this.input = player.input;

        this.rb = player.rb;
        this.animator = player.animator;
    }

    public override void Update()
    {
        base.Update();

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    private bool CanDash()
    {
        if (player.wallDetected)
            return false;

        if (stateMachine.currentState == player.dashState)
            return false;

        return true;
    }

}
