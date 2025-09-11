using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(Enemy enemy, StateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerHit())
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
