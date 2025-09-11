using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy,StateMachine stateMachine, string aniBoolName) : base(stateMachine, aniBoolName)
    {
        this.enemy = enemy;
        rb = enemy.rb;
        animator = enemy.animator;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        float battleAniSpeedMultiplier = enemy.battleSpeed / enemy.moveSpeed;

        animator.SetFloat("moveAniSpeedMultiplier", enemy.moveAniSpeedMultiplier);
        animator.SetFloat("battleAniSpeedMultiplier", battleAniSpeedMultiplier);
        animator.SetFloat("xVelocity", rb.velocity.x);
    }
}
