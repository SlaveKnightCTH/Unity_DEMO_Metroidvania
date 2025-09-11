public class Player_JumpAttackState : PlayerState
{
    private bool touchedGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string aniBoolName) : base(player, stateMachine, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDir, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        //一次跳跃攻击只能运行一次
        if (player.groundDetected && touchedGround == false)
        {
            touchedGround = true;
            animator.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.velocity.y);
        }


        if (triggerCalled && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }



}
