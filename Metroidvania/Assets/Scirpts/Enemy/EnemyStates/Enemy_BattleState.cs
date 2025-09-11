using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform playerTransform;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string aniBoolName) : base(enemy, stateMachine, aniBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log(rb.velocity);
        enemy.SetVelocity(0, rb.velocity.y);
        //Debug.Log(rb.velocity);

        UpdateBatlleTime();

        //通过射线
        //进入时得到player的transform
        //
        playerTransform ??= enemy.GetPlyaerTransform();

        //if (playerTransform == null)
        //{
        //    playerTransform = enemy.GetPlyaerTransform();
        //}

        //失败
        //if (ShouldRetreat())
        //{
        //    Debug.Log("Retreat");
        //    rb.velocity = new Vector2(enemy.retreatVelocity.x * DirectionToPlayer(), enemy.retreatVelocity.y);
        //    enemy.HandleFlip(DirectionToPlayer());
        //}

        
    }

    public override void Update()
    {
        base.Update();



        if (WithinAttackRange() && enemy.PlayerHit())
        {
            enemy.SetVelocity(0, 0);
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleSpeed * DirectionToPlayer(), rb.velocity.y);
        } 

        if (enemy.PlayerHit())
        {
            UpdateBatlleTime();
        }

        if (BattleTimeIsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
        }
         
    }

    private bool BattleTimeIsOver() => enemy.inGameTime > enemy.lastTimeInBattle + enemy.battleTimeDuration;

    private void UpdateBatlleTime()=> enemy.lastTimeInBattle =Time.time;

    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;
    
    private float DistanceToPlayer()
    {
        if (playerTransform == null)
            return float.MaxValue;

        return Mathf.Abs(playerTransform.position.x - enemy.transform.position.x);
    }

    private int DirectionToPlayer()
    {//1在右边 0没有 -1在左边
        if (playerTransform == null)
            return 0;

        return playerTransform.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
