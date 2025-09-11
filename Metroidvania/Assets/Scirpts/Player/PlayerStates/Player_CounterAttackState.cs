using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����/����״̬
//����Q,����״̬

//��ʱ��Ϊ����ʱ�䣬�����˳�״̬
//ִ��player_combat�ķ���ִ�к���
//�������Ϊbool���ص�animator
public class Player_CounterAttackState : PlayerState
{
    private Player_Combat player_Combat;
    private bool counterSomebody;

    public Player_CounterAttackState(Player player, StateMachine stateMachine, string aniBoolName) : base(player, stateMachine, aniBoolName)
    {
        player_Combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player_Combat.GetCounterRecoveryDuration();
        counterSomebody = player_Combat.CounterAttackPerformed();
        animator.SetBool("counterAttackPerformed", counterSomebody);
    }

    public override void Update()
    {
        base.Update();
            
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState); 
        }

        if (stateTimer<0&&counterSomebody==false)
        {
            stateMachine.ChangeState(player.idleState); 
        }
    }
}
