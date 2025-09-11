using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    
    public override void Enter()
    {
        //base.Enter();
        //动画停在上一帧
        animator.enabled = false;

        rb.velocity = new Vector2(rb.velocity.x, 15);
        rb.gravityScale = 12;
        enemy.GetComponent<Collider2D>().enabled = false;

        stateMachine.SwitchOffStateMachine(); 
    }
}
