using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackTimer;
    private float lastAttackTime;
    private float attackVelocityDuration;

    private bool comboAttackQueued;

    private const int ComboIndexMin = 0;
    private int comboIndex = 0;
    private int comboIndexMax = 2;

    private int facingDir;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string aniBoolName) : base(player, stateMachine, aniBoolName)
    {
        if (comboIndexMax + 1 != player.attackVelocity.Length)
        {
            Debug.LogWarning("MaxcomboIndex!=attackVelocity.Length");
            comboIndex = player.attackVelocity.Length;

        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        facingDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDir;

        animator.SetInteger("basicAttackCombaIndex", comboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandelAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();
        }
    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastAttackTime = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            animator.SetBool(aniBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboIndexMax)
            comboAttackQueued = true;
    }

    private void ResetComboIndexIfNeeded()
    {
        //攻击间隔过长，重置
        if (Time.time - lastAttackTime > player.attackResetTime)
            comboIndex = ComboIndexMin;

        if (comboIndex > comboIndexMax)
            comboIndex = ComboIndexMin;
    }

    private void HandelAttackVelocity()
    {
        attackVelocityDuration -= Time.deltaTime;

        if (attackVelocityDuration < 0)
        {
            player.SetVelocity(0, rb.velocity.y);
        }
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityDuration = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity[comboIndex].x * facingDir, player.attackVelocity[comboIndex].y);
    }
}
