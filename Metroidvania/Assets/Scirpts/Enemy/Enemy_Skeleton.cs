using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy,ICounterable
{
    //canbeCounter��canbeStunned��
    //���������ŵ������ж�֡ʱ,canbeStunnedΪ��
    public bool canBeCountered { get => canBeStunned; }
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initailize(idleState);
    }

    [ContextMenu("stun enemy")]
    public void HandleCounter()
    {
        stateMachine.ChangeState(stunnedState);
    }

}
