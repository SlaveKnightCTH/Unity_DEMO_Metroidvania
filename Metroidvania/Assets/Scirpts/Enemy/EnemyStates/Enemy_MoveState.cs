using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        if (!enemy.groundDetected || enemy.wallDetected)
        {
            enemy.Flip();
        }

    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (!enemy.groundDetected||enemy.wallDetected)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.SetVelocity(0, 0);
    }
}
